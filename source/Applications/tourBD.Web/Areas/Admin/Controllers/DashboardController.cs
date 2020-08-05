using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;
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

        public DashboardController(UserManager<ApplicationUser> userManager, ICompanyRequestService companyRequestService)
        {
            _companyRequestService = companyRequestService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RequestList()
        {
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
                           string userData = user.FullName + "$" + user.ImageUrl;
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

        public async Task<IActionResult> Details(string Id)
        {
            var request = _companyRequestService.Get(new Guid(Id));
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                var model = new CompanyRequestDetailViewModel()
                {
                    UserImageUrl = user.ImageUrl,
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
    }
}
