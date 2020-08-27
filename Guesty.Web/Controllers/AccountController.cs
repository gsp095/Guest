using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Guesty.Data.Models;
using Guesty.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Guesty.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public AccountController(
         UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         RoleManager<IdentityRole> roleManager


         )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _RoleManager = roleManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp(string returnUrl = null)
        {
            ViewBag.Role = _RoleManager.Roles.Select(s => s.Name).ToList();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email,FirstName= model.FirstName,LastName= model.LastName };
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,


                };
                var existUser = await _userManager.FindByEmailAsync(model.Email);
                //if (existUser!=null)
                //{
                //    ModelState.AddModelError(string.Empty, "Email already already exist.");
                //    return View(model);
                //}
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, model.Role);
                    await _userManager.AddClaimAsync(user, new Claim("FirstName", user.FirstName));
                    await _userManager.AddClaimAsync(user, new Claim("LastName", user.LastName));
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);


                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ViewBag.Role = _RoleManager.Roles.Select(s => s.Name).ToList();
                    ModelState.AddModelError(string.Empty, "Email already exist.");
                    return View(model);
                }
            }
            //if  something failed then fill country again
            ViewBag.Role = _RoleManager.Roles.Select(s => s.Name).ToList();
            // If we got this far, something failed, redisplay form
            return View("SignUp", model);

        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied(string returnUrl = null)
        {
           
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true              
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    var user = await _userManager.FindByNameAsync(model.Email);
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));

                    if (role == "Owner") { return RedirectToAction(nameof(HomeController.Owner), "Home"); }
                    else if (role == "Team") { return RedirectToAction(nameof(HomeController.Team), "Home"); }
                    else if (role == "Manager") { return RedirectToAction(nameof(HomeController.Manager), "Home"); }
                    else if (role == "Guest") { return RedirectToAction(nameof(Areas.Guest.Controllers.RegisterController.Index), "Register", new { area = "Guest" }); }
                    else { return RedirectToAction(nameof(HomeController.Index), "Home"); }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                    // return Json(Json("Invalid login attempt."));

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
            //return Json(Json("Invalid login attempt."));
        }


    }
}