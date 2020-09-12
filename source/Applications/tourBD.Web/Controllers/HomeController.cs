using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tourBD.Membership.Entities;
using tourBD.Membership.Services;

namespace tourBD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPathService _pathService;

        public HomeController(UserManager<ApplicationUser> userManager, IPathService pathService)
        {
            _userManager = userManager;
            _pathService = pathService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
                user.ImageUrl = $"{_pathService.PictureFolder}{user.ImageUrl}";

            return View();
        }
    }
}
