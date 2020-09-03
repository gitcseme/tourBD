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
        private readonly IPathService _pathService;

        public CompanyController(
            UserManager<ApplicationUser> userManager, 
            ICompanyRequestService companyRequestService,
            ICompanyService companyService,
            ITourPackageService tourPackageService,
            IWebHostEnvironment webHostEnvironment,
            IPathService pathService)
        {
            _userManager = userManager;
            _companyRequestService = companyRequestService;
            _companyService = companyService;
            _tourPackageService = tourPackageService;
            _webHostEnvironment = webHostEnvironment;
            _pathService = pathService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = (await _companyService.GetUserCompaniesAsync(user.Id)).ToList();
            model.ForEach(c => c.CompanyImageUrl = $"{_pathService.PictureFolder}{c.CompanyImageUrl}");

            return View(model);
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
                    RequestStatus = CompanyRequestStatus.Pending.ToString()
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
            model.Company.CompanyImageUrl = $"{_pathService.PictureFolder}{model.Company.CompanyImageUrl}";

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
                string imagePath = _pathService.PictureFolder;
                string physicalUploadPath = _webHostEnvironment.WebRootPath + imagePath;
                string demoImage = _pathService.DummyCompanyImageUrl;

                model.Company.CompanyImageUrl = await GeneralUtilityMethods.GetSavedImageUrlAsync(model.ImageFile, physicalUploadPath, imagePath, demoImage);
                await _companyService.EditAsync(model.Company);

                return RedirectToAction("CompanyPublicView", "Company", new { companyId = model.Company.Id.ToString() });
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
                tourPackage.PackageCode = GeneralUtilityMethods.GeneratePackageCode();
                tourPackage.Availability = AvailabilityStatus.Available.ToString();
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

                return RedirectToAction("CompanyPublicView", "Company", new { companyId = tourPackage.CompanyId });
            }

            return View(tourPackage);
        }

        public async Task<IActionResult> DeletePackage(string packageId)
        {
            var tourPackage = await _tourPackageService.GetPackageWithRelatedSpotsAsync(new Guid(packageId));
            foreach (var spot in tourPackage.Spots.ToList()) // to list creates a copy of the data.
            {
                await _tourPackageService.DeleteSpotAsync(spot);
            }

            await _tourPackageService.DeleteAsync(tourPackage);
            return RedirectToAction("CompanyPublicView", "Company", new { companyId = tourPackage.CompanyId });
        }

        [HttpGet]
        public async Task<IActionResult> EditPackage(string packageId)
        {
            var tourPackage = await _tourPackageService.GetPackageWithRelatedSpotsAsync(new Guid(packageId));
            var model = new EditPackageViewModel()
            {
                packageId = tourPackage.Id,
                PackageCode = tourPackage.PackageCode,
                MainArea = tourPackage.MainArea,
                Days = tourPackage.Days,
                Price = tourPackage.Price,
                Availability = tourPackage.Availability,
                Discount = tourPackage.Discount,
                Spots = (from s in tourPackage.Spots
                        select s.Name).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> EditPackage(EditPackageViewModel model)
        {
            var tourPackage = await _tourPackageService.GetPackageWithRelatedSpotsAsync(model.packageId);

            // delete all spots
            foreach (var spot in tourPackage.Spots.ToList())
            {
                await _tourPackageService.DeleteSpotAsync(spot);
            }

            // Rebuild the package spots from model
            tourPackage.MainArea = model.MainArea;
            tourPackage.Days = model.Days;
            tourPackage.Price = model.Price;
            tourPackage.Availability = model.Availability;
            tourPackage.Discount = model.Discount;
            foreach (var spot in model.Spots)
            {
                var newSpot = new Spot()
                {
                    Name = spot,
                    TourPackageId = tourPackage.Id
                };

                await _tourPackageService.AddSpot(newSpot);
            }

            await _tourPackageService.EditAsync(tourPackage);
            return RedirectToAction("CompanyPublicView", "Company", new { companyId = tourPackage.CompanyId });
        }

        [HttpGet]
        public async Task<IActionResult> CompanyPublicView(string companyId)
        {
            var company = await _companyService.GetCompanyWithAllIncludePropertiesAsync(new Guid(companyId));
            company.CompanyImageUrl = $"{_pathService.PictureFolder}{company.CompanyImageUrl}";

            company.TourPackages.ForEach(tp =>
            {
                tp.Spots.Sort((s1, s2) => s1.Name.Length.CompareTo(s2.Name.Length));
            });

            return View(company);
        }
    }
}
