using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using login.web.Data;
using login.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace login.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelpers _userHelpers;

        public AccountController(DataContext context,
                                 IUserHelpers userHelpers)
        {
            this._userHelpers = userHelpers;
        }

        // GET: Account
        public  ActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");

            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _userHelpers.LoginPasswordSAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index","Home");
                }

            }

            ModelState.AddModelError(string.Empty,"Fialed to login");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelpers.LogoutSigOutAsync();

            return RedirectToAction("Index","Home");
        }
    }
}