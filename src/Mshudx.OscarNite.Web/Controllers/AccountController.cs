using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Mshudx.OscarNite.Web.Security;
using Mshudx.OscarNite.Web.ViewModels.Account;
using Microsoft.Extensions.OptionsModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Mshudx.OscarNite.Web.Controllers
{
    public class AccountController : Controller
    {

        private SignInManager<ApplicationUser> signInManager;
        private IOptions<Security.PasswordOptions> passwordOptions;

        public AccountController(SignInManager<ApplicationUser> signInManager, IOptions<Security.PasswordOptions> passwordOptions)
        {
            this.signInManager = signInManager;
            this.passwordOptions = passwordOptions;
        }

        public async Task<IActionResult> Login()
        {
            await Task.Yield();
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                
                if (model.Code == passwordOptions.Value.Admin)
                {
                    await signInManager.SignInAsync(ApplicationUser.Admin, false);
                    return RedirectToAction("Index", "Admin");
                }
                if (model.Code == passwordOptions.Value.Reporting)
                {
                    await signInManager.SignInAsync(ApplicationUser.Report, false);
                    return RedirectToAction("Index", "Reporting");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
