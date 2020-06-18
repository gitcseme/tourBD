using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Web.Models;

namespace tourBD.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register(string returnUrl = null)
        {
            var model = new RegisterModel() { ReturnUrl = returnUrl };
            return View(model);
        }
    }
}
