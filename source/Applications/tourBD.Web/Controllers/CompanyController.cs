using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tourBD.Membership.Entities;
using tourBD.Membership.Services;
using tourBD.Web.Models.CompanyModels;

namespace tourBD.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyRequestService _companyRequestService;
        private readonly ICompanyService _companyService;

        public CompanyController(
            UserManager<ApplicationUser> userManager, 
            ICompanyRequestService companyRequestService,
            ICompanyService companyService)
        {
            _userManager = userManager;
            _companyRequestService = companyRequestService;
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var companyList = await _companyService.GetUserCompaniesAsync(user.Id);

            return View(companyList);
        }

        [HttpGet]
        public async Task<IActionResult> RequestCompany()
        {
            var model = new CompanyRequestModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RequestCompany(CompanyRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var request = new CompanyRequest()
                {
                    UserId = user.Id,
                    Description = model.Description,
                    RequestDate = DateTime.Now,
                    RequestStatus = Membership.Enums.CompanyRequestStatus.Pending.ToString()
                };

                await _companyRequestService.CreateAsync(request);
                return RedirectToAction("Index", "Company");
            }

            return View(model);
        }
    }
}
