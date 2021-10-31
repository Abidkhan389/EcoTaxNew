using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.Utilities;
using GreenLife.Utilities.Services;
using GreenLife.ViewModels;
using GreenLife.ViewModels.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    public class ConformationController : Controller
    {
        private readonly EFCoreCityRepository _eFCoreCityRepository;
        private readonly EFCoreCustomerRepository _eFCoreCustomerRepository;
        private readonly EFCoreCityRepository _eFCoreCityRepository1;
        private readonly EFCoreStoreRepository _eFCoreStoreRepository;
        private readonly EFCoreOrdersRepository _eFCoreOrdersRepository;
        private readonly EFCoreOrderItemsRepository _eFCoreOrderItemsRepository;
        private readonly IMailService _mailService;

        public ConformationController(EFCoreCityRepository eFCoreCityRepository,
            EFCoreCustomerRepository eFCoreCustomerRepository, EFCoreStoreRepository eFCoreStoreRepository,
             EFCoreOrdersRepository eFCoreOrdersRepository, EFCoreOrderItemsRepository eFCoreOrderItemsRepository
            , IMailService mailService)
        {
            this._eFCoreCityRepository = eFCoreCityRepository;
            this._eFCoreCustomerRepository = eFCoreCustomerRepository;
            this._eFCoreStoreRepository = eFCoreStoreRepository;
            this._eFCoreOrdersRepository = eFCoreOrdersRepository;
            this._eFCoreOrderItemsRepository = eFCoreOrderItemsRepository;
            this._mailService = mailService;
        }
        public async Task<IActionResult> Index(SalesCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Item> cart = (List<Item>)HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

                SalesCustomer newcustomer = new SalesCustomer
                {
                    CustomerId = model.CustomerId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email,
                    Street = model.Street,
                    Cityid = model.Cityid,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    TransactionTID = model.TransactionTID,
                    paymentmode = model.paymentmode.ToString(),

                };
                await _eFCoreCustomerRepository.Add(newcustomer);

                SalesOrder salesOrder = new SalesOrder();
                DateTime shipdate = DateTime.Now.AddDays(5);
                var city = await _eFCoreCityRepository.Get(newcustomer.Cityid ?? 0);
                salesOrder.CustomerId = newcustomer.CustomerId;
                salesOrder.ShippedDate = shipdate;
                if (city.CityName == "Multan" || city.CityName == "Khanewal")
                {
                    salesOrder.StoreId = (await _eFCoreStoreRepository.Get(city.Id)).StoreId;
                }
                salesOrder.OrderStatus = 0;
                await _eFCoreOrdersRepository.Add(salesOrder);
                List<SalesOrderItem> salesOrderItems = new List<SalesOrderItem>();

                foreach (var order in cart)
                {
                    SalesOrderItem salesOrderItem = new SalesOrderItem();
                    salesOrderItem.OrderId = salesOrder.OrderId;
                    salesOrderItem.ProductId = order.product.ProductId;
                    salesOrderItem.Quantity = order.Quantity;
                    salesOrderItem.ListPrice = order.product.ListPrice;
                    salesOrderItems.Add(salesOrderItem);
                }
                await _eFCoreOrderItemsRepository.AddAll(salesOrderItems);

                //email to client
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = newcustomer.Email;                
                var fullName = newcustomer.FirstName + " " + newcustomer.LastName;
                mailRequest.Body = $@"<html>
                                    <body>
                                    <p>Dear ,{fullName}</p>
                                    <p>We sincerely hope you are satisfied with your purchase. Thanks for shopping at EcoTax. We at EcoTax know you had many options to choose from, we thank you for choosing us.</p>
                                    <p>Your order will be received with in 5 days.</p>
                                    <p>Sincerely,<br>EcoTax</br></p>
                                    </body>
                                    </html>";
                await _mailService.SendEmailAsync(mailRequest);                
                

                //send to admin for prepare order
                
                mailRequest.ToEmail = "farhatkhanwazir@gmail.com";                
                //var fullName = newcustomer.FirstName + " " + newcustomer.LastName;
                mailRequest.Body = $@"<html>
                                    <body>
                                    <p>Dear Admin</p>
                                    <p>Oder id:{salesOrder.OrderId} is prepared. Please find customer details here.</p>
                                    <p>Customer Name: {fullName}</p>
                                    <p>Customer Mobile: {newcustomer.Phone}</p>
                                    <p>Customer City: {newcustomer.City}</p>
                                    <p>Customer Address: {newcustomer.Street}</p>
                                    <p>Sincerely,<br>EcoTax</br></p>
                                    </body>
                                    </html>";
                await _mailService.SendEmailAsync(mailRequest);
                HttpContext.Session.Clear();
                //HttpContext.Session.Clear("cart");
                
                return RedirectToAction("Index");
            }
            else
                return View();

        }
    }
}
