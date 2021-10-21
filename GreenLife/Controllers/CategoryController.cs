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
    public class CategoryController : Controller
    {
        private readonly EFCoreCategoryRepository _repository;

        public CategoryController(EFCoreCategoryRepository repository)
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
            ProductionCategoryViewModel newcategory = new ProductionCategoryViewModel();
            return View(newcategory);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductionCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductionCategory newcateogory = new ProductionCategory()
                {
                    CategoryName = model.CategoryName,
                };
                await _repository.Add(newcateogory);
                return RedirectToAction("Index", new { id = newcateogory.CategoryId });
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductionCategory editcategory = await _repository.Get(id);
            ProductionCategoryViewModel category = new ProductionCategoryViewModel
            {
                CategoryId = editcategory.CategoryId,
                CategoryName = editcategory.CategoryName
            };
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductionCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductionCategory editcategory = await _repository.Get(model.CategoryId);
                editcategory.CategoryName = model.CategoryName;
                await _repository.Update(editcategory);
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
                ProductionCategory category = await _repository.Get(id);
                if (category != null)
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
