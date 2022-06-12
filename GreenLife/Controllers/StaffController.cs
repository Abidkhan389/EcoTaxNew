using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly EFCoreStaffRepository _repository;
        private readonly EFCoreStoreRepository _eFCoreStoreRepository;

        public StaffController(EFCoreStaffRepository repository, EFCoreStoreRepository eFCoreStoreRepository)
        {
            this._repository = repository;
            this._eFCoreStoreRepository = eFCoreStoreRepository;
        }
        public async Task<IActionResult> Profile(int id)
        {
            var obj = await _repository.Get(id);
            return View(obj);
        }

        public async Task<IActionResult> Index()
        {
            List<SalesStaffViewModel> staffList = new List<SalesStaffViewModel>();
            var obj = await _repository.GetAll();
            SalesStore storeobj = new SalesStore();
            foreach (var item in obj)
            {
                SalesStaffViewModel staffloop = new SalesStaffViewModel();
                staffloop.FirstName = item.FirstName;
                staffloop.LastName = item.LastName;
                staffloop.Email = item.Email;
                staffloop.Phone = item.Phone;
                if (item.Active == 1)
                {
                    staffloop.choseactivetype = true;
                }
                else
                    staffloop.choseactivetype = false;
                storeobj = await _eFCoreStoreRepository.Get(item.StoreId);
                staffloop.storesName = storeobj.StoreName;

                //staffloop.StoreId = item.StoreId;
                //staffloop.ManagerId = item.ManagerId;
                staffloop.StaffId = item.StaffId;
                staffloop.Active = item.Active;
                staffList.Add(staffloop);
            }
            return View(staffList);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {


            SalesStaffViewModel model = new SalesStaffViewModel();
            var storelistall = await _eFCoreStoreRepository.GetAll();
            List<stores> storeList = new List<stores>();
            foreach (var item in storelistall)
            {
                stores newstore = new stores();
                newstore.StoreName = item.StoreName;
                newstore.Id = item.StoreId;
                storeList.Add(newstore);
            }
            model.storelist = storeList;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SalesStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                SalesStaff staff = new SalesStaff
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,

                    StoreId = model.StoreId,
                   // ManagerId = model.ManagerId
                };
                if (model.choseactivetype)
                {
                    staff.Active = 1;
                }
                else
                {
                    staff.Active = 0;
                }
                await _repository.Add(staff);
                return RedirectToAction("Index", new { id = staff.StaffId });
            }
            else
                return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SalesStaff editstaff = await _repository.Get(id);
            var storelistall = await _eFCoreStoreRepository.GetAll();
            List<stores> storeList = new List<stores>();
            foreach (var item in storelistall)
            {
                stores newstore = new stores();
                newstore.StoreName = item.StoreName;
                newstore.Id = item.StoreId;
                storeList.Add(newstore);
            }
            SalesStaffViewModel staff = new SalesStaffViewModel();

            staff.FirstName = editstaff.FirstName;
            staff.LastName = editstaff.LastName;
            staff.Email = editstaff.Email;
            staff.Phone = editstaff.Phone;
            if (staff.choseactivetype)
            {
                staff.Active = 1;
            }
            else
            {
                staff.Active = 0;
            }
            staff.storelist = storeList;
            //StoreId = editstaff.StoreId,
            //staff.ManagerId = editstaff.ManagerId;
            staff.StaffId = editstaff.StaffId;


            return View(staff);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SalesStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                SalesStaff updatestaff = await _repository.Get(model.StaffId);
                updatestaff.FirstName = model.FirstName;
                updatestaff.LastName = model.LastName;
                updatestaff.Email = model.Email;
                updatestaff.Phone = model.Phone;
                if (model.choseactivetype)
                {
                    updatestaff.Active = 1;
                }
                else
                {
                    updatestaff.Active = 0;
                }
                updatestaff.StoreId = model.StoreId;
                //updatestaff.ManagerId = model.ManagerId;
                updatestaff.StaffId = model.StaffId;
                await _repository.Update(updatestaff);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                SalesStaff deletestaff = await _repository.Get(id);
                if (deletestaff != null)
                {
                    await _repository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
