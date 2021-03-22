using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using tourBD.Forum.Services;
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
        private readonly IPostService _postService;
        private readonly ICompanyService _companyService;

        public HomeController(
            UserManager<ApplicationUser> userManager, 
            IPathService pathService, 
            IConfiguration configuration, 
            IPostService postService, 
            ICompanyService companyService)
        {
            _userManager = userManager;
            _pathService = pathService;
            _configuration = configuration;
            _postService = postService;
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            await PrepareLoggedInUserAsync();
            int numberOfPost = 5;

            var model = new HomeModel
            {
                Posts = (await _postService.GetRecentPostsAsync(numberOfPost)).Select(p =>
                    new PostModel {
                        AuthorName = p.AuthorName,
                        AuthorImageUrl = p.AuthorImageUrl.Contains(_pathService.PictureFolder) ? p.AuthorImageUrl : $"{_pathService.PictureFolder}/{p.AuthorImageUrl}",
                        Message = p.Message.Length > 50 ? p.Message.Substring(0, 50) + "..." : p.Message,
                        PostId = p.Id.ToString()
                    }).ToList(),

                Companies = (await _companyService.GetAllAsync()).Select(c =>
                    new CompanyModel
                    {
                        Id = c.Id.ToString(),
                        CompanyName = c.Name,
                        CompanyLogoUrl = $"{_pathService.LogoFolder}/{c.CompanyLogo}"
                    }).ToList()
            };

            return View(model);
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

        public async Task<IActionResult> Contact()
        {
            await PrepareLoggedInUserAsync();

            var tourBDInfo = new TourBDInfo();
            _configuration.Bind(nameof(tourBDInfo), tourBDInfo);
            tourBDInfo.Developer.ImageUrl = $"{_pathService.PictureFolder}/{tourBDInfo.Developer.ImageUrl}";

            return View(tourBDInfo);
        }
    }
}
