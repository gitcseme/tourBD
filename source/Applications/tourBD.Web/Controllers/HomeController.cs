using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tourBD.Forum.Entities;
using tourBD.Forum.Services;
using tourBD.Membership.Entities;
using tourBD.Web.Models;
using tourBD.Web.Models.PostModels;

namespace tourBD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private IPostService _postService;

        public HomeController(
            ILogger<HomeController> logger, 
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IPostService postService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            string include = "Comments";
            var model = await _postService.GetAllIncludePropertiesAsync(includeProperties: include, isTrackingOff: true);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePost(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = new CreatePostViewModel { 
                UserId = userId, 
                AuthorName = user.FullName, 
                AuthorImageUrl = user.ImageUrl
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newPost = new Post()
                {
                    AuthorId = new Guid(model.UserId),
                    AuthorName = model.AuthorName,
                    AuthorImageUrl = model.AuthorImageUrl,
                    Message = model.Message,
                    CreationDate = DateTime.Now
                };

                await _postService.CreateAsync(newPost);
                return RedirectToAction("Index", "Home");
            }

            return View("CreatePost", model);
        }

        public IActionResult AddLike(string Id)
        {
            _postService.AddLike(Id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
