using GreenLife.ViewModels;
using GreenLife.Data.EFCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EFCoreUserRepository _repository;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                  EFCoreUserRepository repository, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _repository = repository;
            _roleManager = roleManager;
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"this { email} is already taken");
            }
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        //[AllowAnonymous]
        public async Task<IActionResult> Register()

        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            //var result = await _repository.GetAll();
            var result = _roleManager.Roles;
            foreach (var item in result)
            {
                UserRoleModel userRoleModel = new UserRoleModel();
                userRoleModel.roleId = item.Id;
                userRoleModel.name = item.Name;
                registerViewModel.userRoles.Add(userRoleModel);
            }

            return View(registerViewModel);
        }
        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.UserCity
                };
                var Result = await userManager.CreateAsync(user, model.Password);
                if (Result.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, model.RoleId);
                    //if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListRoles", "Administration");
                    //}
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("AdminIndex", "Product");
                }
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var user = await userManager.FindByEmailAsync(model.Email);
                        var roles = await userManager.GetRolesAsync(user);
                        return RedirectToAction("AdminIndex", "Product", new { area = "" });
                        //return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "invalid login attempt");
                }
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
