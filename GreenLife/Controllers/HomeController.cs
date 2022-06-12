using GreenLife.Data;
using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    public  class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EfCoreProductRepository _efCoreProductRepository;
        private readonly GreenLifeFinalContext _context;

        public HomeController(ILogger<HomeController> logger, EfCoreProductRepository efCoreProductRepository , GreenLifeFinalContext context)
        {
            _logger = logger;
            this._efCoreProductRepository = efCoreProductRepository;
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductionProductViewModel> productList = new List<ProductionProductViewModel>();
            //var obj3 = await _context.ProductionProducts.OrderByDescending(x => x.ProductId).ToListAsync();
            var obj3 = (await _efCoreProductRepository.GetAll()).OrderByDescending(x=>x.ProductId).ToList();
           var obj = await _efCoreProductRepository.GetAll();
            
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
    }
}
