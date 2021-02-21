using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Forum.Services;
using tourBD.Membership.Entities;
using tourBD.Membership.Enums;
using tourBD.Membership.Services;
using tourBD.Web.Areas.Admin.Models;

namespace tourBD.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ICompanyRequestService _companyRequestService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager _roleManager;
        private readonly ICompanyService _companyService;
        private readonly IPathService _pathService;
        private readonly ITourPackageService _tourPackageService;
        private readonly IPostService _postService;
        private readonly IAccountService _accountService;

        public DashboardController(
            UserManager<ApplicationUser> userManager,
            RoleManager roleManager,
            ICompanyRequestService companyRequestService,
            ICompanyService companyService,
            IPathService pathService,
            ITourPackageService tourPackageService,
            IPostService postService,
            IAccountService accountService)
        {
            _companyRequestService = companyRequestService;
            _userManager = userManager;
            _companyService = companyService;
            _pathService = pathService;
            _tourPackageService = tourPackageService;
            _postService = postService;
            _accountService = accountService;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            await GetLoggedInUser();
            var userRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            Dictionary<string, List<Person>> dictionary = new Dictionary<string, List<Person>>();
            foreach (var role in userRoles)
            {
                var applicationUsers = (await _userManager.GetUsersInRoleAsync(role)).ToList();
                var persons = applicationUsers.Select(au => new Person
                {
                    Id = au.Id.ToString(),
                    Name = au.FullName,
                    ImageUrl = au.ImageUrl.Contains("img") ? au.ImageUrl : $"{_pathService.PictureFolder}/{au.ImageUrl}"
                }).ToList();

                dictionary.Add(role, persons);
            }

            var model = new AdminHomeModel
            {
                TotalCompanies = await _companyService.GetCountAsync(),
                TotalPackages = await _tourPackageService.GetCountAsync(),
                TotalPosts = await _postService.GetCountAsync(),
                TotalRegisteredUsers = _userManager.Users.Count(),
                Authorities = dictionary
            };


            return View(model);
        }

        public async Task<IActionResult> RequestList()
        {
            await GetLoggedInUser();

            return View();
        }

        public  JsonResult GetRequests()
        {
            DatatableAjaxRequestModel request = new DatatableAjaxRequestModel(Request);
            var requestData = _companyRequestService.GetRequests(request.PageIndex, request.PageSize, true, request.SearchText, "", "");

            var result = new
            {
                recordsTotal = requestData.Item2,
                recordsFiltered = requestData.Item3,
                data = requestData.Item1.Select(r =>
                       {
                           var user = _userManager.FindByIdAsync(r.UserId.ToString()).Result;
                           string userData = user.FullName + "$" + $"{_pathService.PictureFolder}{user.ImageUrl}" + "$" + user.Id.ToString();
                           string description = r.Description.Length < 40 ? r.Description : r.Description.Substring(0, 35) + "...";

                           return new string[]
                           {
                               userData,
                               description,
                               r.RequestDate.ToString(),
                               r.RequestStatus,
                               r.Id.ToString()
                           };
                       })
            };

            return Json(result);
        }

        public async Task<IActionResult> GetUserList()
        {
            await GetLoggedInUser();

            return View();
        }

        public async Task<JsonResult> GetUsers()
        {
            DatatableAjaxRequestModel request = new DatatableAjaxRequestModel(Request);
            var requestData = await _accountService.GetUsersAsync(request.PageIndex, request.PageSize, true, request.SearchText, request.SortColumnName, request.OrderDirection);

            var result = new
            {
                recordsTotal = requestData.Item2,
                recordsFiltered = requestData.Item3,
                data = requestData.Item1.Select(u =>
                {
                    string userData = u.FullName + "$" + $"{_pathService.PictureFolder}{u.ImageUrl}" + "$" + u.Id.ToString();

                    return new string[]
                    {
                        userData,
                        u.Email,
                        u.PhoneNumber,
                        u.Address,
                        u.IsVarified.ToString()
                    };
                })
            };

            return Json(result);
        }

        public async Task<IActionResult> GetPostList()
        {
            await GetLoggedInUser();

            return View();
        }

        public async Task<JsonResult> GetPosts()
        {
            DatatableAjaxRequestModel request = new DatatableAjaxRequestModel(Request);
            var requestData = await _postService.GetPostsAsync(request.PageIndex, request.PageSize, true, request.SearchText, request.SortColumnName, request.OrderDirection);

            var result = new
            {
                recordsTotal = requestData.Item2,
                recordsFiltered = requestData.Item3,
                data = requestData.Item1.Select(post =>
                {
                    string userData = post.AuthorName + "$" + $"{_pathService.PictureFolder}{post.AuthorImageUrl}" + "$" + post.AuthorId.ToString();
                    string message = post.Message.Length < 50 ? post.Message : post.Message.Substring(0, 50) + "...";

                    return new string[]
                    {
                        userData,
                        message,
                        post.CreationDate.ToString(),
                        post.Id.ToString()
                    };
                })
            };

            return Json(result);
        }

        public async Task<IActionResult> Details(string Id)
        {
            await GetLoggedInUser();

            var request = _companyRequestService.Get(new Guid(Id));
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                var model = new CompanyRequestDetailViewModel()
                {
                    UserId = user.Id.ToString(),
                    UserImageUrl = user.ImageUrl.Contains(_pathService.PictureFolder) ? user.ImageUrl : $"{_pathService.PictureFolder}{user.ImageUrl}",
                    UserName = user.FullName,
                    CompanyRequestId = request.Id.ToString(),
                    Description = request.Description,
                    RequestedDate = request.RequestDate,
                    RequestStatus = request.RequestStatus
                };

                return View(model);
            }

            return RedirectToAction("RequestList", "Dashboard");
        }

        public async Task<IActionResult> ApproveRequest(string requestId)
        {
            var companyRequestEntity = _companyRequestService.Get(new Guid(requestId));
            companyRequestEntity.RequestStatus = CompanyRequestStatus.Approved.ToString(); // chnage status to Approved.
            await _companyRequestService.EditAsync(companyRequestEntity);

            // create a new company
            Company company = new Company()
            {
                Name = "not set",
                Address = "not set",
                PhoneNumber = "not set",
                Email = "not set",
                CompanyImageUrl = _pathService.DummyCompanyImageUrl,
                Star = 0,
                UserId = companyRequestEntity.UserId,
                CompanyLogo = _pathService.DummyCompanyLogo
            };

            await _companyService.CreateAsync(company);
            return RedirectToAction("RequestList", "Dashboard");
        }

        public async Task<IActionResult> RejectRequest(string requestId)
        {
            var companyRequestEntity = _companyRequestService.Get(new Guid(requestId));
            companyRequestEntity.RequestStatus = CompanyRequestStatus.Rejected.ToString(); // chnage status to Rejected.
            await _companyRequestService.EditAsync(companyRequestEntity);

            return RedirectToAction("RequestList", "Dashboard");
        }

        private async Task GetLoggedInUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.ImageUrl = $"{_pathService.PictureFolder}{user.ImageUrl}";
        }
    }
}
