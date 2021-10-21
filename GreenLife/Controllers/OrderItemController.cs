using GreenLife.Data.EFCore;
using GreenLife.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{

    [Authorize]
    public class OrderItemController : Controller
    {
        private readonly EFCoreOrderItemsRepository _repository;
        //private readonly EFCoreOrdersRepository _eFCoreOrdersRepository;

        public OrderItemController(EFCoreOrderItemsRepository repository)
        {
            this._repository = repository;
            //this._eFCoreOrdersRepository = eFCoreOrdersRepository;
        }
        // GET: OrderItemController
        public async Task<ActionResult> Index()
        {
            var obj = await _repository.GetAll();
            return View();
        }



        // GET: OrderItemController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderItemController/Create
        [HttpPost]

        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderItemController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderItemController/Edit/5
        [HttpPost]

        public ActionResult Edit(SalesOrderItemViewModel model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // POST: OrderItemController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
