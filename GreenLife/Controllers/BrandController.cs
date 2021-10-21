using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GreenLife.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private readonly EFCoreBrandRepository _repository;

        public BrandController(EFCoreBrandRepository repository)
        {
            this._repository = repository;

        }
        public async Task<IActionResult> Index()
        {
            var obj = await _repository.GetAll();
            return View(obj);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductionBrandViewModel brand = new ProductionBrandViewModel();
            return View(brand);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductionBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductionBrand newbrand = new ProductionBrand
                {
                    BrandName = model.BrandName,

                };
                await _repository.Add(newbrand);
                return RedirectToAction("Index", new { id = newbrand.BrandId });
            }
            else
                return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductionBrand editbrand = await _repository.Get(id);
            ProductionBrandViewModel brand = new ProductionBrandViewModel
            {
                BrandName = editbrand.BrandName,
                BrandId = editbrand.BrandId,
            };
            return View(brand);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductionBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductionBrand editbrand = await _repository.Get(model.BrandId);
                editbrand.BrandName = model.BrandName;
                await _repository.Update(editbrand);
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
                ProductionBrand deletebrand = await _repository.Get(id);
                if (deletebrand != null)
                {
                    await _repository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            else
                return View();
        }
    }
}
