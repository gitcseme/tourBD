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
using tourBD.NotificationChannel.Services;
using tourBD.Web.Models;
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
        private readonly INotificationService _notificationService;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IPathService pathService,
            IConfiguration configuration,
            IPostService postService,
            ICompanyService companyService, INotificationService notificationService)
        {
            _userManager = userManager;
            _pathService = pathService;
            _configuration = configuration;
            _postService = postService;
            _companyService = companyService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var loggedinUser = await PrepareLoggedInUserAsync();
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

            if (loggedinUser != null)
            {
                await LayoutBaseModelLoaderHelper.LoadBaseAsync(model, loggedinUser.Id, _notificationService, _pathService);
            }

            return View(model);
        }

        public async Task<IActionResult> Services()
        {
            var user = await PrepareLoggedInUserAsync();
            var model = new LayoutBaseModel();
            if (user != null)
                await LayoutBaseModelLoaderHelper.LoadBaseAsync(model, user.Id, _notificationService, _pathService);

            return View(model);
        }

        private async Task<ApplicationUser> PrepareLoggedInUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
                user.ImageUrl = $"{_pathService.PictureFolder}{user.ImageUrl}";

            return user;
        }

        public async Task<IActionResult> Contact()
        {
            var user = await PrepareLoggedInUserAsync();

            var tourBDInfo = new TourBDInfo();
            _configuration.Bind(nameof(tourBDInfo), tourBDInfo);
            tourBDInfo.Developer.ImageUrl = $"{_pathService.PictureFolder}/{tourBDInfo.Developer.ImageUrl}";

            if (user != null)
                await LayoutBaseModelLoaderHelper.LoadBaseAsync(tourBDInfo, user.Id, _notificationService, _pathService);

            return View(tourBDInfo);
        }
    }
}
