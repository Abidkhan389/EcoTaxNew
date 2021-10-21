using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> logger;
        private readonly EFCoreStoreRepository _repository;
        private readonly EFCoreCityRepository _eFCoreCityRepository;

        public StoreController(ILogger<StoreController> logger, EFCoreStoreRepository repository, EFCoreCityRepository eFCoreCityRepository)
        {
            this.logger = logger;
            this._repository = repository;
            this._eFCoreCityRepository = eFCoreCityRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<SalesStoreViewModel> StoreList = new List<SalesStoreViewModel>();
            var obj = await _repository.GetAll();
            City cityobj = new City();
            foreach (var item in obj)
            {
                SalesStoreViewModel storeloop = new SalesStoreViewModel();
                storeloop.StoreName = item.StoreName;
                storeloop.Phone = item.Phone;
                storeloop.Email = item.Email;
                storeloop.Street = item.Street;


                cityobj = await _eFCoreCityRepository.Get(item.Cityid ?? 0); //take city name thorugh cityid,
                storeloop.citiesname = cityobj.CityName; // put cityname according to cityid

                storeloop.State = item.State;
                storeloop.ZipCode = item.ZipCode;
                storeloop.StoreId = item.StoreId;
                StoreList.Add(storeloop);
            }
            return View(StoreList);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            SalesStoreViewModel model = new SalesStoreViewModel();
            var citieslistall = await _eFCoreCityRepository.GetAll();
            List<storecities> cityList = new List<storecities>();
            foreach (var item in citieslistall)
            {
                storecities newcity = new storecities();
                newcity.CityName = item.CityName;
                newcity.Id = item.Id;
                cityList.Add(newcity);
            }
            model.citieslist = cityList;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SalesStoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                SalesStore newstore = new SalesStore
                {
                    StoreName = model.StoreName,
                    Phone = model.Phone,
                    Email = model.Email,
                    Street = model.Street,
                    Cityid = model.Cityid,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    StoreId = model.StoreId,
                };
                await _repository.Add(newstore);
                return RedirectToAction("Index", new { id = newstore.StoreId });
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SalesStore editstore = await _repository.Get(id);
            var citieslistall = await _eFCoreCityRepository.GetAll();
            List<storecities> cityList = new List<storecities>();
            foreach (var item in citieslistall)
            {
                storecities newcity = new storecities();
                newcity.CityName = item.CityName;
                newcity.Id = item.Id;
                cityList.Add(newcity);
            }
            SalesStoreViewModel store = new SalesStoreViewModel
            {
                StoreName = editstore.StoreName,
                Phone = editstore.Phone,
                Email = editstore.Email,
                Street = editstore.Street,
                citieslist = cityList,
                Cityid = editstore.Cityid,
                State = editstore.State,
                ZipCode = editstore.ZipCode,
                StoreId = editstore.StoreId,
            };
            return View(store);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SalesStoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                SalesStore editstore = await _repository.Get(model.StoreId);
                editstore.StoreName = model.StoreName;
                editstore.Phone = model.Phone;
                editstore.Email = model.Email;
                editstore.Street = model.Street;
                editstore.Cityid = model.Cityid;
                editstore.State = model.State;
                editstore.ZipCode = model.ZipCode;
                await _repository.Update(editstore);
                return RedirectToAction("Index");
            }
            else
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                SalesStore deletestore = await _repository.Get(id);
                if (deletestore != null)
                {
                    await _repository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
