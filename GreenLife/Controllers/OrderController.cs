using GreenLife.Data.EFCore;
using GreenLife.Models;
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
    public class OrderController : Controller
    {
        private readonly EFCoreOrdersRepository _repository;
        private readonly EFCoreStoreRepository _eFCoreStoreRepository;
        private readonly EFCoreStaffRepository _eFCoreStaffRepository;
        private readonly EFCoreCustomerRepository _eFCoreCustomerRepository;

        public OrderController(EFCoreOrdersRepository repository, EFCoreStoreRepository eFCoreStoreRepository,
            EFCoreStaffRepository eFCoreStaffRepository, EFCoreCustomerRepository eFCoreCustomerRepository)
        {
            this._repository = repository;
            this._eFCoreStoreRepository = eFCoreStoreRepository;
            this._eFCoreStaffRepository = eFCoreStaffRepository;
            this._eFCoreCustomerRepository = eFCoreCustomerRepository;
        }
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            var obj = await _repository.GetAll();

            List<SalesOrderViewModel> OrderList = new List<SalesOrderViewModel>();
            SalesStaff staffobj = new SalesStaff();
            SalesCustomer customerobj = new SalesCustomer();
            SalesStore storeobj = new SalesStore();
            foreach (var item in obj)
            {
                SalesOrderViewModel orderloop = new SalesOrderViewModel();
                orderloop.OrderId = item.OrderId;

                //geting customer first&last name  thorugh customerid from customer table
                customerobj = await _eFCoreCustomerRepository.Get(item.CustomerId ?? 0);
                orderloop.customerfirstname = customerobj.FirstName;
                orderloop.customerlastname = customerobj.LastName;


                if (item.OrderStatus == 1)
                {
                    orderloop.choseorderstatus = true;
                }
                else
                    orderloop.choseorderstatus = false;

                orderloop.OrderDate = item.OrderDate;
                orderloop.RequiredDate = item.RequiredDate;
                orderloop.ShippedDate = item.ShippedDate;

                //getting staff first& last name from staff table through staffid
                staffobj = await _eFCoreStaffRepository.Get(item.StaffId);
                orderloop.staffirstname = staffobj.FirstName;
                orderloop.staflastname = staffobj.LastName;

                //geting storename list thorugh storeid from store table
                storeobj = await _eFCoreStoreRepository.Get(item.StoreId);
                orderloop.storename = storeobj.StoreName;

                OrderList.Add(orderloop);
            }
            return View(OrderList);
        }

        // GET: OrderController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            SalesOrderViewModel model = new SalesOrderViewModel();
            var customerListAll = await _eFCoreCustomerRepository.GetAll();
            var StoreListAll = await _eFCoreStoreRepository.GetAll();
            var StaffListAll = await _eFCoreStaffRepository.GetAll();
            List<customer> customerlist = new List<customer>();
            List<Store> storelist = new List<Store>();
            List<staff> stafflist = new List<staff>();
            foreach (var item in customerListAll)
            {
                customer newcustomer = new customer();
                newcustomer.CustomerId = item.CustomerId;
                newcustomer.FirstName = item.FirstName;
                newcustomer.LastName = item.LastName;
                customerlist.Add(newcustomer);
            }
            foreach (var item in StaffListAll)
            {
                staff newstaff = new staff();
                newstaff.StaffId = item.StaffId;
                newstaff.FirstName = item.FirstName;
                newstaff.LastName = item.LastName;
                stafflist.Add(newstaff);
            }
            foreach (var item in StoreListAll)
            {
                Store newstore = new Store();
                newstore.StoreId = item.StoreId;
                newstore.StoreName = item.StoreName;

                storelist.Add(newstore);
            }
            model.storeslist = storelist;
            model.stafflist = stafflist;
            model.customerslist = customerlist;
            return View(model);
        }

        // POST: OrderController/Create
        [HttpPost]

        public async Task<ActionResult> Create(SalesOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                SalesOrder neworder = new SalesOrder
                {
                    OrderId = model.OrderId,
                    CustomerId = model.CustomerId,
                    OrderDate = model.OrderDate,
                    RequiredDate = model.RequiredDate,
                    ShippedDate = model.ShippedDate,
                    StoreId = model.StoreId,
                    StaffId = model.StaffId,
                };
                if (model.choseorderstatus)
                {
                    neworder.OrderStatus = 1;
                }
                else
                {
                    neworder.OrderStatus = 0;
                }

                await _repository.Add(neworder);
                return RedirectToAction("Index", new { id = neworder.OrderId });
            }
            else
                return View();
        }

        // GET: OrderController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            SalesOrder editorder = await _repository.Get(id);
            var customerListAll = await _eFCoreCustomerRepository.GetAll();
            var StoreListAll = await _eFCoreStoreRepository.GetAll();
            var StaffListAll = await _eFCoreStaffRepository.GetAll();
            List<customer> customerlist = new List<customer>();
            List<Store> storelist = new List<Store>();
            List<staff> stafflist = new List<staff>();
            foreach (var item in customerListAll)
            {
                customer newcustomer = new customer();
                newcustomer.CustomerId = item.CustomerId;
                newcustomer.FirstName = item.FirstName;
                newcustomer.LastName = item.LastName;
                customerlist.Add(newcustomer);
            }
            foreach (var item in StaffListAll)
            {
                staff newstaff = new staff();
                newstaff.StaffId = item.StaffId;
                newstaff.FirstName = item.FirstName;
                newstaff.LastName = item.LastName;
                stafflist.Add(newstaff);
            }
            foreach (var item in StoreListAll)
            {
                Store newstore = new Store();
                newstore.StoreId = item.StoreId;
                newstore.StoreName = item.StoreName;

                storelist.Add(newstore);
            }
            SalesOrderViewModel updateorder = new SalesOrderViewModel();
            updateorder.customerslist = customerlist;
            updateorder.CustomerId = editorder.CustomerId;
            updateorder.OrderId = editorder.OrderId;
            if (editorder.OrderStatus == 1)
            {
                updateorder.choseorderstatus = true;
            }
            else
            {
                updateorder.choseorderstatus = false;
            }


            updateorder.OrderDate = editorder.OrderDate;
            updateorder.RequiredDate = editorder.RequiredDate;
            updateorder.ShippedDate = editorder.ShippedDate;
            updateorder.storeslist = storelist;
            updateorder.StoreId = editorder.StoreId;
            updateorder.stafflist = stafflist;
            updateorder.StaffId = editorder.StaffId;

            return View(updateorder);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SalesOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                SalesOrder editorder = await _repository.Get(model.OrderId);

                editorder.OrderId = model.OrderId;
                editorder.CustomerId = model.CustomerId;
                editorder.OrderDate = model.OrderDate;
                editorder.RequiredDate = model.RequiredDate;
                editorder.ShippedDate = model.ShippedDate;
                editorder.StoreId = model.StoreId;
                editorder.StaffId = model.StaffId;

                if (model.choseorderstatus)
                {
                    editorder.OrderStatus = 1;
                }
                else
                {
                    editorder.OrderStatus = 0;
                }
                //editorder.OrderStatus = model.OrderStatus;
                await _repository.Update(editorder);
                return RedirectToAction("Index", new { id = editorder.OrderId });
            }
            else
                return View();
        }



        // POST: OrderController/Delete/5
        [HttpPost]

        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                SalesOrder deleteorder = await _repository.Delete(id);
                if (deleteorder != null)
                {
                    await _repository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ViewResult> Details(int? id)
        {
            SalesOrder order = await _repository.Get(id ?? 0);
            if (order == null)
            {
                Response.StatusCode = 404;
                return View("NotFound", id.Value);
            }
            SalesOrderViewModel orderdetails = new SalesOrderViewModel();

            SalesStaff staffobj = new SalesStaff();
            SalesCustomer customerobj = new SalesCustomer();
            SalesStore storeobj = new SalesStore();
            //geting customer first&last name  thorugh customerid from customer table
            customerobj = await _eFCoreCustomerRepository.Get(order.CustomerId ?? 0);
            orderdetails.customerfirstname = customerobj.FirstName;
            orderdetails.customerlastname = customerobj.LastName;
            orderdetails.CustomerId = customerobj.CustomerId;

            //getting staff first& last name from staff table through staffid
            staffobj = await _eFCoreStaffRepository.Get(order.StaffId);
            orderdetails.staffirstname = staffobj.FirstName;
            orderdetails.staflastname = staffobj.LastName;
            orderdetails.StaffId = staffobj.StaffId;

            //geting storename list thorugh storeid from store table
            storeobj = await _eFCoreStoreRepository.Get(order.StoreId);
            orderdetails.storename = storeobj.StoreName;
            orderdetails.StoreId = storeobj.StoreId;

            orderdetails.OrderId = order.OrderId;
            if (order.OrderStatus == 1)
            {
                orderdetails.choseorderstatus = true;
            }
            else
            {
                orderdetails.choseorderstatus = false;
            }

            orderdetails.OrderDate = order.OrderDate;
            orderdetails.RequiredDate = order.RequiredDate;
            orderdetails.ShippedDate = order.ShippedDate;
            return View(orderdetails);
        }
    }
}
