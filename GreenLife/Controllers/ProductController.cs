using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    public class ProductController :Controller  /*HomeController<ProductionProduct, EfCoreProductRepository>*/
    {
        private readonly GreenLifeFinalContext _greenLifeFinalContext;
        private readonly EfCoreProductRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly EFCoreBrandRepository _eFCoreBrandRepository;
        private readonly EFCoreCategoryRepository _eFCoreCategoryRepository;
        private readonly GreenLifeFinalContext _context;

        public ProductController(EfCoreProductRepository repository, IHostingEnvironment hostingEnvironment,
            EFCoreBrandRepository eFCoreBrandRepository ,EFCoreCategoryRepository eFCoreCategoryRepository, GreenLifeFinalContext context) 
        {
            this._repository = repository;
            this._hostingEnvironment = hostingEnvironment;
            this._eFCoreBrandRepository = eFCoreBrandRepository;
            this._eFCoreCategoryRepository = eFCoreCategoryRepository;
            this._context = context;
        }
        public async Task<IActionResult> Index ()
        {
            List<ProductionProductViewModel> productList = new List<ProductionProductViewModel>();
            var obj3 = await _context.ProductionProducts.OrderByDescending(x => x.ProductId).ToListAsync();
            foreach (var item in obj3)
            {
                ProductionProductViewModel productlistloop = new ProductionProductViewModel();
                productlistloop.ProductName = item.ProductName;
                productlistloop.ProductId = item.ProductId;
                productlistloop.ListPrice = item.ListPrice;
                productlistloop.Productphoto = item.Photo;
                productList.Add(productlistloop);
            }
            return View(productList);
        }
        [AllowAnonymous]
        public async Task<IActionResult> AdminIndex ()
        {
            List<ProductionProductViewModel> productList = new List<ProductionProductViewModel>();
            var obj = await _repository.GetAll();

            ProductionBrand brandobj = new ProductionBrand();
            ProductionCategory cateogyrobj = new ProductionCategory();

            foreach (var item in obj)
            {
                ProductionProductViewModel productlistloop = new ProductionProductViewModel();
                productlistloop.ProductName = item.ProductName;
                productlistloop.ProductId = item.ProductId;

                brandobj=await _eFCoreBrandRepository.Get(item.BrandId);
                productlistloop.BrandName = brandobj.BrandName;

                cateogyrobj =await _eFCoreCategoryRepository.Get(item.CategoryId);
                productlistloop.CateogryName = cateogyrobj.CategoryName;

                productlistloop.ModelYear = item.ModelYear;
                productlistloop.ListPrice = item.ListPrice;
                productlistloop.Productphoto = item.Photo;

                productList.Add(productlistloop);
            }

            

            return View(productList);
        }
        private string ProcessFileUpload(ProductionProductViewModel model)
        {
            string uniquefilename = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images/Product/");
                uniquefilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filepath = Path.Combine(uploadsFolder, uniquefilename);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    model.Photo.CopyTo(filestream);
                }
            }
            return uniquefilename;
        }
        [HttpGet]
        public async Task <IActionResult> Create()
        {
            ProductionProductEditViewModel Model = new ProductionProductEditViewModel();
            var categorylistAll = await _eFCoreCategoryRepository.GetAll(); 
             var barndlistAll = await _eFCoreBrandRepository.GetAll();

            List<category> categoryList = new List<category>();
            List<brand> brandList = new List<brand>();

            //string UniqeFilename = ProcessFileUpload(Model);

            foreach (var item in categorylistAll)
            {
                category category = new category();
                category.CategoryId = item.CategoryId;
                category.CategoryName = item.CategoryName;

                categoryList.Add(category);
            }
            foreach (var item in barndlistAll)
            {
                brand brand = new brand();
                brand.BrandId = item.BrandId;
                brand.BrandName = item.BrandName;


                brandList.Add(brand);
            }
            Model.categorylist = categoryList;
            Model.brandlist = brandList;
            return View(Model);
        }
        [HttpPost]
        public async  Task<IActionResult> Create(ProductionProductViewModel Model)
        {

            if (ModelState.IsValid)
            {
                string UniqeFilename = ProcessFileUpload(Model);
                ProductionProduct newProduct = new ProductionProduct()
                {
                    //ProductId=Model.Id,
                    ProductName=Model.ProductName,
                    BrandId=Model.BrandId,
                    CategoryId=Model.CategoryId,
                    ModelYear=Model.ModelYear,
                    ListPrice=Model.ListPrice,
                    Photo = UniqeFilename,
                };
                await _repository.Add (newProduct);
                return RedirectToAction("AdminIndex", new { id = newProduct.ProductId });
            }
            else
            {
                return View();
            }


        }
        [HttpGet] 
        public async Task <ViewResult> Edit(int id)
        {
            ProductionProduct product = await _repository.Get(id);
            var categorylistAll = await _eFCoreCategoryRepository.GetAll();
            var barndlistAll = await _eFCoreBrandRepository.GetAll();

            List<category> categoryList = new List<category>();
            List<brand> brandList = new List<brand>();

            //string UniqeFilename = ProcessFileUpload(Model);

            foreach (var item in categorylistAll)
            {
                category category = new category();
                category.CategoryId = item.CategoryId;
                category.CategoryName = item.CategoryName;

                categoryList.Add(category);
            }
            foreach (var item in barndlistAll)
            {
                brand brand = new brand();
                brand.BrandId = item.BrandId;
                brand.BrandName = item.BrandName;


                brandList.Add(brand);
            }
            ProductionProductEditViewModel ProductEditViewModel = new ProductionProductEditViewModel
            {
                //Id = product.ProductId,
                ProductName = product.ProductName,
                categorylist = categoryList,
                brandlist = brandList,
                //BrandId = product.BrandId,
                //CategoryId = product.CategoryId,
                ModelYear = product.ModelYear,
                ListPrice = product.ListPrice,
                ExistingPhtopathEdit = product.Photo,
               
                ExistingID=product.ProductId
            };
            return View(ProductEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductionProductEditViewModel model)
        {
            //if(model.Photo == null)
            //{
            //    model.Photo = model.EditPhoto;
            //}
            if (ModelState.IsValid)
            {
                ProductionProduct product = await _repository.Get(model.ExistingID);
                product.ProductName = model.ProductName;
                product.BrandId = model.BrandId;
                product.CategoryId = model.CategoryId;
                product.ModelYear = model.ModelYear;
                product.ListPrice = model.ListPrice;
                if (model.Photo != null)
                {
                    if (model.ExistingPhtopathEdit != null)
                    {
                        string filepath = Path.Combine(_hostingEnvironment.WebRootPath, "Images/Product/", model.ExistingPhtopathEdit);
                        System.IO.File.Delete(filepath);
                    }
                    product.Photo = ProcessFileUpload(model);
                }
                await _repository.Update(product);

                return RedirectToAction("AdminIndex");
            }

            return View();

        }
       [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                ProductionProduct product = await _repository.Get(id);
                if (product != null)
                {
                    await _repository.Delete(id);
                }
                return RedirectToAction("AdminIndex");
            }
            return View();
        }
    }
}
