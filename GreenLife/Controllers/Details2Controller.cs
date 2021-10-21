using GreenLife.Data.EFCore;
using GreenLife.ViewModels;
using GreenLife.ViewModels.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GreenLife.Models;

namespace GreenLife.Controllers
{

    public class Details2Controller : Controller
    {
        private readonly ILogger<Details2Controller> logger;
        private readonly EfCoreProductRepository _efCoreProductRepository;
        private readonly EFCoreCategoryRepository _eFCoreCategoryRepository;
        private readonly GreenLifeFinalContext _greenLifeFinalContext;

        public Details2Controller(ILogger<Details2Controller> logger, EfCoreProductRepository efCoreProductRepository,
            EFCoreCategoryRepository eFCoreCategoryRepository, GreenLifeFinalContext greenLifeFinalContext)
        {
            _greenLifeFinalContext = greenLifeFinalContext;
            this.logger = logger;
            this._efCoreProductRepository = efCoreProductRepository;
            this._eFCoreCategoryRepository = eFCoreCategoryRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            //List<ProductionProductViewModel> productList = new List<ProductionProductViewModel>();
            var obj = await _eFCoreCategoryRepository.GetAll();
            var productObj = await _efCoreProductRepository.Get(id);
            List<category> cateogyrList = new List<category>();
            foreach (var item in obj)
            {
                category newcategory = new category();
                newcategory.CategoryId = item.CategoryId;
                newcategory.CategoryName = item.CategoryName;
                cateogyrList.Add(newcategory);
            }
            ProductionProductViewModel model = new ProductionProductViewModel
            {
                ProductName = productObj.ProductName,
                ProductId = productObj.ProductId,
                ListPrice = productObj.ListPrice,
                Productphoto = productObj.Photo,

                categorylist = cateogyrList
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Addtocart(ProductionProductViewModel model)
        {

            if (HttpContext.Session.GetObjectFromJson<List<Item>>("cart") == null)
            {
                List<Item> cart = new List<Item>();
                var product = await _efCoreProductRepository.Get(model.ProductId);
                ProductCheckOut productCheckOut = new ProductCheckOut();
                productCheckOut.CategoryName = (await _eFCoreCategoryRepository.Get(product.CategoryId)).CategoryName;
                productCheckOut.ProductName = product.ProductName;
                productCheckOut.ListPrice = product.ListPrice;
                productCheckOut.ProductId = product.ProductId;
                cart.Add(new Item()
                {
                    product = productCheckOut,
                    Quantity = model.numberofquantity

                });
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                List<Item> cart = (List<Item>)HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
                var product = await _efCoreProductRepository.Get(model.ProductId);
                bool ItemNotExcsist = true;
                foreach (Item item in cart.ToList())
                {
                    if (item.product.ProductId == model.ProductId)
                    {
                        item.Quantity = item.Quantity + model.numberofquantity;
                        int previousqunetity = item.Quantity;
                        ItemNotExcsist = false;
                        //cart.RemoveAll(item);
                        //cart.Add(new Item()
                        //{
                        //    product = product,
                        //    Quantity = model.numberofquantity + previousqunetity
                        //}) ;

                        break;
                    }
                    //else
                    //{
                    //    cart.Add(new Item()
                    //    {
                    //        product = product,
                    //        Quantity = model.numberofquantity,
                    //    });
                    //}
                }
                if (ItemNotExcsist)
                {
                    ProductCheckOut productCheckOut = new ProductCheckOut();
                    productCheckOut.CategoryName = (await _eFCoreCategoryRepository.Get(product.CategoryId)).CategoryName;
                    productCheckOut.ProductName = product.ProductName;
                    productCheckOut.ListPrice = product.ListPrice;
                    productCheckOut.ProductId = product.ProductId;

                    cart.Add(new Item()
                    {
                        product = productCheckOut,
                        Quantity = model.numberofquantity,
                    });
                    ItemNotExcsist = false;
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return RedirectToAction("Index", new { id = model.ProductId });
        }

        [HttpPost]
        public JsonResult RemoveFromCart(int id)
        {
            try
            {
                List<Item> cart = (List<Item>)HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
                foreach (var item in cart)
                {
                    if (item.product.ProductId == id)
                    {
                        cart.Remove(item);
                        break;
                    }
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }



        }
    }
}
