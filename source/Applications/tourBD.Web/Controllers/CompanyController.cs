using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tourBD.Core.Utilities;
using tourBD.Membership.Entities;
using tourBD.Membership.Enums;
using tourBD.Membership.Services;
using tourBD.Web.Models.CompanyModels;

namespace tourBD.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyRequestService _companyRequestService;
        private readonly ICompanyService _companyService;
        private readonly ITourPackageService _tourPackageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyController(
            UserManager<ApplicationUser> userManager, 
            ICompanyRequestService companyRequestService,
            ICompanyService companyService,
            ITourPackageService tourPackageService,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _companyRequestService = companyRequestService;
            _companyService = companyService;
            _tourPackageService = tourPackageService;
            _webHostEnvironment = webHostEnvironment;
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

        [HttpGet]
        public async Task<IActionResult> EditCompany(string companyId)
        {
            var model = new EditCompanyViewModel() 
            { 
                Company = await _companyService.GetCompanyWithAllIncludePropertiesAsync(new Guid(companyId))
            };

            model.Company.TourPackages.Sort((t1, t2) => t1.PackageNumber.CompareTo(t2.PackageNumber));
            model.Company.TourPackages.ForEach(tp =>
            {
                tp.Spots.Sort((s1, s2) => s1.Name.Length.CompareTo(s2.Name.Length));
            });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCompany(EditCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imagePath = @"\img\Upload\";
                string physicalUploadPath = _webHostEnvironment.WebRootPath + imagePath;
                string demoImage = @"\img\companyImage.jpg";

                model.Company.CompanyImageUrl = await GeneralUtilityMethods.GetSavedImageUrlAsync(model.ImageFile, physicalUploadPath, imagePath, demoImage);
                await _companyService.EditAsync(model.Company);

                return RedirectToAction("EditCompany", "Company", new { companyId = model.Company.Id.ToString() }); // should be redirect to [ViewCompany]
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePackage(string companyId)
        {
            TourPackage tourPackage = new TourPackage() { CompanyId = new Guid(companyId) };
            ViewBag.companyId = companyId;
            return View(tourPackage);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage(TourPackage tourPackage)
        {
            if (ModelState.IsValid)
            {
                tourPackage.Id = Guid.NewGuid();
                tourPackage.Availability = AvailabilityStatus.Available.ToString();
                tourPackage.PackageNumber = (await _companyService.GetCompanyWithAllIncludePropertiesAsync(tourPackage.CompanyId)).TourPackages.Count + 1;
                var Spots = tourPackage.Spots;
                tourPackage.Spots = null; // creates PK_Spot conflict otherwise

                // Create the tourPackage first.
                await _companyService.CreateTourPackage(tourPackage);

                // Add the spots
                foreach (var spot in Spots)
                {
                    spot.TourPackage = null; // Create PK_Company conflict otherwise
                    spot.TourPackageId = tourPackage.Id;
                    await _tourPackageService.AddSpot(spot);
                }

                return RedirectToAction("EditCompany", "Company", new { companyId = tourPackage.CompanyId }); // should be redirect to [ViewCompany]
            }

            return View(tourPackage);
        }

        public async Task<IActionResult> DeletePackage(string packageId)
        {
            var tourPackage = await _tourPackageService.GetPackageWithRelatedSpotsAsync(new Guid(packageId));
            foreach (var spot in tourPackage.Spots.ToList()) // to list creates a copy of the data.
            {
                //spot.TourPackage = null; // to avoid PK conflict
                await _tourPackageService.DeleteSpotAsync(spot);
            }

            await _tourPackageService.DeleteAsync(tourPackage);
            return RedirectToAction("EditCompany", "Company", new { companyId = tourPackage.CompanyId }); // should be redirect to [ViewCompany]
        }
    }
}
