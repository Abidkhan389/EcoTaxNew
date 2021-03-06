using AutoMapper;
using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.ViewModels.City;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private readonly EFCoreCityRepository _repository;
        private readonly IMapper mapper; // this is the object of automapperprofile class to mapping automatically

        public CityController(EFCoreCityRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var obj = await _repository.GetAll();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CityCreateViewModel newcityy = new CityCreateViewModel();
            return View(newcityy);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CityCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //City newCity = mapper.Map<City>(model);// mapp viewmodel object with the help of mapper class wiht automapperProfile mapper 
                //await _repository.Add(newCity);// aftrer mapping object, we call the generic method for add city in city tble in database
                City newcity = new()
                {
                    CityName = model.cityname

                };
                await _repository.Add(newcity);
                return RedirectToAction("Index", new { id = newcity.Id });
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            City editcity = await _repository.Get(id);
            CityCreateViewModel city = new CityCreateViewModel
            {
                cityid = editcity.Id,
                cityname = editcity.CityName
            };
            return View(city);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CityCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                City editcity = await _repository.Get(model.cityid);
                editcity.CityName = model.cityname;
                await _repository.Update(editcity);
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
                City newcity = await _repository.Get(id);
                if (newcity != null)
                {
                    await _repository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
