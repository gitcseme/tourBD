﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tourBD.Core.Utilities;
using tourBD.Forum.Entities;
using tourBD.Forum.Services;
using tourBD.Membership.Entities;
using tourBD.Membership.Services;
using tourBD.Web.Models;
using tourBD.Web.Models.PostModels;

namespace tourBD.Web.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ForumController> _logger;
        private IPostService _postService;
        private readonly IPathService _pathService;

        public ForumController(
            ILogger<ForumController> logger, 
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IPostService postService,
            IPathService pathService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _postService = postService;
            _pathService = pathService;
        }

        /* Redesign the index page */
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            var posts = await _postService.GetAllPostsPaginatedAsync(pageIndex, pageSize);

            var model = posts.Select(post => PreparePostViewModel(post, loggedInUser)).ToList();

            loggedInUser.ImageUrl = $"{_pathService.PictureFolder}{loggedInUser.ImageUrl}";

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
            user.ImageUrl = $"{_pathService.PictureFolder}{user.ImageUrl}";

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
                return RedirectToAction("Index", "Forum");
            }

            return View("CreatePost", model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewPost(string postId)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            var post = await _postService.GetPostIncludePropertiesAsync(new Guid(postId));
            var model = PreparePostViewModel(post, loggedInUser);

            loggedInUser.ImageUrl = $"{_pathService.PictureFolder}{loggedInUser.ImageUrl}";

            return View(model);
        }

        public async Task<IActionResult> AddLikeAsync(string postId)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var like = new Like()
            {
                AuthorId = loggedInUser.Id,
                PostId = new Guid(postId)
            };

            await _postService.AddLikeAsync(like);

            return RedirectToAction("Index", "Forum");
        }

        public async Task<IActionResult> AddComment(string postId, string message)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var comment = new Comment()
            {
                AuthorId = loggedInUser.Id,
                AuthorName = loggedInUser.FullName,
                AuthorImageUrl = GetImageName(loggedInUser.ImageUrl),
                CreationDate = DateTime.Now,
                Message = message,
                PostId = new Guid(postId)
            };

            await _postService.AddCommentAsync(comment);

            return RedirectToAction("Index", "Forum");
        }

        public async Task<IActionResult> AddReplay(string commentId, string message)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var replay = new Replay()
            {
                AuthorId = loggedInUser.Id,
                AuthorName = loggedInUser.FullName,
                AuthorImageUrl = GetImageName(loggedInUser.ImageUrl),
                CreationDate = DateTime.Now,
                Message = message,
                CommentId = new Guid(commentId)
            };

            await _postService.AddReplayAsync(replay);
            return RedirectToAction("Index", "Forum");
        }


        public async Task<IActionResult> DeleteReplay (string replayId)
        {
            await _postService.DeleteReplayAsync(new Guid(replayId));
            return RedirectToAction("Index", "Forum");
        }

        private string GetImageName(string imageUrl)
        {
            return imageUrl.Contains(_pathService.PictureFolder) ? imageUrl.Substring(_pathService.PictureFolder.Length) : imageUrl;
        }

        private PostViewModel PreparePostViewModel(Post post, ApplicationUser loggedInUser)
        {
            return new PostViewModel
            {
                PostId = post.Id,
                AuthorId = post.AuthorId,
                AuthorName = post.AuthorName,
                AuthorImageUrl = $"{_pathService.PictureFolder}{post.AuthorImageUrl}",
                CreationDate = GeneralUtilityMethods.GetFormattedDate(post.CreationDate),
                Message = post.Message,
                Likes = post.Likes.Count,
                IsLikedBy = post.Likes.Where(l => l.AuthorId == loggedInUser.Id).Any(),
                IsPostAuthor = post.AuthorId == loggedInUser.Id,
                Comments = post.Comments.Select(cmt => new CommentViewModel
                {
                    CommentId = cmt.Id,
                    PostId = cmt.PostId,
                    AuthorId = cmt.AuthorId,
                    AuthorName = cmt.AuthorName,
                    AuthorImageUrl = $"{_pathService.PictureFolder}{cmt.AuthorImageUrl}",
                    CreationDate = GeneralUtilityMethods.GetFormattedDate(cmt.CreationDate),
                    Message = cmt.Message,
                    IsCommentAuthor = cmt.AuthorId == loggedInUser.Id,
                    Replays = cmt.Replays.Select(rpl => new ReplayViewModel
                    {
                        ReplayId = rpl.Id,
                        CommentId = rpl.CommentId,
                        AuthorId = rpl.AuthorId,
                        AuthorName = rpl.AuthorName,
                        AuthorImageUrl = $"{_pathService.PictureFolder}{rpl.AuthorImageUrl}",
                        CreationDate = GeneralUtilityMethods.GetFormattedDate(rpl.CreationDate),
                        Message = rpl.Message,
                        IsReplayAuthor = rpl.AuthorId == loggedInUser.Id
                    }).ToList()
                }).ToList()
            };
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
