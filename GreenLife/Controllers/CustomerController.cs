using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.Utilities;
using GreenLife.Utilities.Services;
using GreenLife.ViewModels;
using GreenLife.ViewModels.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly EFCoreCustomerRepository _repository;
        private readonly IMailService _mailService;
        private readonly GreenLifeFinalContext _greenLifeFinalContext;
        private readonly EFCoreCityRepository _eFCoreCityRepository;

        private readonly EfCoreProductRepository _efCoreProductRepository;

        public CustomerController(EFCoreCustomerRepository repository, IMailService mailService,
            GreenLifeFinalContext greenLifeFinalContext, EFCoreCityRepository eFCoreCityRepository, EfCoreProductRepository efCoreProductRepository)
        {
            this._repository = repository;
            this._mailService = mailService;
            this._greenLifeFinalContext = greenLifeFinalContext;
            this._eFCoreCityRepository = eFCoreCityRepository;
            this._efCoreProductRepository = efCoreProductRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> Send(string email)
        {
            try
            {
                Random random = new Random();
                int randomNumber = random.Next(1000, 999999);
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = email;
                HttpContext.Session.SetString("verificationCode", randomNumber.ToString());
                mailRequest.Body = "This is your verification Code:" + randomNumber.ToString();
                await _mailService.SendEmailAsync(mailRequest);
                //var verificationCode= HttpContext.Session.GetString("verificationCode");
                return Json(true);
            }
            catch (Exception ex)
            {

                return Json(false);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult VerifyCode(string code)
        {
            try
            {
               
                string verifycode=HttpContext.Session.GetString("verificationCode");
                if (code == verifycode)
                    return Json(true);
                else
                    return Json(false);
            }
            catch (Exception ex)
            {

                return Json(false);
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult VerifyTransactionTID(string TransactionId)
        {
            try
            {

                var verifyTransactionId =  _greenLifeFinalContext.SalesCustomers.Where(x => x.TransactionTID == TransactionId).ToListAsync();
                if (verifyTransactionId != null)
                    return Json(true);
                else
                    return Json(false);
            }
            catch (Exception ex)
            {

                return Json(false);
            }

        }
        //[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> checkout()
        {
            List<Item> cart = (List<Item>)HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

            SalesCustomerViewModel model = new SalesCustomerViewModel();
           
            var citieslistall = await _eFCoreCityRepository.GetAll();
            List<cities> cityList = new List<cities>();
            foreach (var item in citieslistall)
            {
                cities newcity = new cities();
                newcity.CityName = item.CityName;
                newcity.Id = item.Id;
                cityList.Add(newcity);
            }

            model.citieslist = cityList;

            return View(model);
        }

        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            var obj = await _repository.GetAll();
            List<SalesCustomerViewModel> CustomerList = new List<SalesCustomerViewModel>();

            City cityobj = new City();
            foreach (var item in obj)
            {
                SalesCustomerViewModel customerloop = new SalesCustomerViewModel();
                customerloop.FirstName = item.FirstName;
                customerloop.LastName = item.LastName;
                customerloop.Phone = item.Phone;
                customerloop.Email = item.Email;
                customerloop.Street = item.Street;

                cityobj = await _eFCoreCityRepository.Get(item.Cityid ?? 0); //take city name thorugh cityid,
                customerloop.citiesname = cityobj.CityName; // put cityname according to cityid

                customerloop.State = item.State;
                customerloop.ZipCode = item.ZipCode;
                customerloop.CustomerId = item.CustomerId;
                CustomerList.Add(customerloop);
            }
            return View(CustomerList);

        }

        // GET: CustomerController/Create
        // [HttpGet]
        // public async Task< ActionResult> Create()
        // {
        //     SalesCustomerViewModel model = new SalesCustomerViewModel();
        //     var citieslistall = await _eFCoreCityRepository.GetAll();
        //     List<cities> cityList = new List<cities>();
        //     foreach (var item in citieslistall)
        //     {
        //         cities newcity = new cities();
        //         newcity.CityName = item.CityName;
        //         newcity.Id = item.Id;
        //         cityList.Add(newcity);
        //     }
        //     model.citieslist = cityList;
        //     return View(model);

        // }

        // // POST: CustomerController/Create
        // [HttpPost]
        //// [ValidateAntiForgeryToken]
        // public async Task< ActionResult> Create(SalesCustomerViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         SalesCustomer newcustomer = new SalesCustomer
        //         {
        //             CustomerId=model.CustomerId,
        //             FirstName=model.FirstName,
        //             LastName=model.LastName,
        //             Phone=model.Phone,
        //             Email=model.Email,
        //             Street=model.Street,
        //             Cityid=model.Cityid,
        //             State=model.State,
        //             ZipCode=model.ZipCode
        //         };
        //         await _repository.Add(newcustomer);
        //         return RedirectToAction("Index", new { id = newcustomer.CustomerId });

        //     }
        //     else
        //         return View();
        // }

        // GET: CustomerController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            SalesCustomer editcustomer = await _repository.Get(id);
            var citieslistall = await _eFCoreCityRepository.GetAll();
            List<cities> cityList = new List<cities>();
            foreach (var item in citieslistall)
            {
                cities newcity = new cities();
                newcity.CityName = item.CityName;
                newcity.Id = item.Id;
                cityList.Add(newcity);
            }
            SalesCustomerViewModel updatecustomer = new SalesCustomerViewModel
            {
                CustomerId = editcustomer.CustomerId,
                FirstName = editcustomer.FirstName,
                LastName = editcustomer.LastName,
                Phone = editcustomer.Phone,
                Email = editcustomer.Email,
                Street = editcustomer.Street,
                citieslist = cityList,
                Cityid = editcustomer.Cityid,
                State = editcustomer.State,
                ZipCode = editcustomer.ZipCode
            };
            return View(updatecustomer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SalesCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                SalesCustomer editcustomer = await _repository.Get(model.CustomerId);
                editcustomer.CustomerId = model.CustomerId;
                editcustomer.FirstName = model.FirstName;
                editcustomer.LastName = model.LastName;
                editcustomer.Phone = model.Phone;
                editcustomer.Email = model.Email;
                editcustomer.Street = model.Street;
                editcustomer.Cityid = model.Cityid;
                editcustomer.State = model.State;
                editcustomer.ZipCode = model.ZipCode;
                await _repository.Update(editcustomer);
                return RedirectToAction("Index");
            }
            else
                return View();
        }


        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                SalesCustomer deletecustomer = await _repository.Get(id);
                if (deletecustomer != null)
                {
                    await _repository.Delete(id);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
