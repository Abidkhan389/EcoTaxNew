using GreenLife.Data.EFCore;
using GreenLife.Models;
using GreenLife.ViewModels.ContactUs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly EFCoreMessagesRepository _repository;

        public ContactUsController(EFCoreMessagesRepository repository)
        {
            this._repository = repository;
        }
        

        public async Task<IActionResult> MessageShow ()
        {
            var obj = await _repository.GetAll();
            return View(obj);
        }


        [HttpGet]
        public async Task< IActionResult> Index()
        {
            ContactUsViewModel model = new ContactUsViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Messages newmassage = new Messages
                {
                    ID=model.ID,
                    firstname = model.firstname,
                    lastname = model.lastname,
                    email=model.email,
                    address= model.address,
                    phonenumber=model.phonenumber,
                    message=model.message
                };
                await _repository.Add(newmassage);
                return RedirectToAction("Index");
            }
            else
                return View();
            
        }
    }
}
