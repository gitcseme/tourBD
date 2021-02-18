using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using tourBD.Membership.Entities;
using tourBD.Membership.Services;
using tourBD.Web.Models.Home;

namespace tourBD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPathService _pathService;
        private readonly IConfiguration _configuration;

        public HomeController(UserManager<ApplicationUser> userManager, IPathService pathService, IConfiguration configuration)
        {
            _userManager = userManager;
            _pathService = pathService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            await PrepareLoggedInUserAsync();

            return View();
        }

        public async Task<IActionResult> Services()
        {
            await PrepareLoggedInUserAsync();

            return View();
        }

        private async Task PrepareLoggedInUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
                user.ImageUrl = $"{_pathService.PictureFolder}{user.ImageUrl}";
        }

        public IActionResult Contact()
        {
            var tourBDInfo = new TourBDInfo();
            _configuration.Bind(nameof(tourBDInfo), tourBDInfo);
            tourBDInfo.Developer.ImageUrl = $"{_pathService.PictureFolder}/{tourBDInfo.Developer.ImageUrl}";

            return View(tourBDInfo);
        }
    }
}
