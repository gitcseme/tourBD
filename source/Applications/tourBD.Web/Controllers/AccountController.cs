using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tourBD.Core.Utilities;
using tourBD.Membership.Entities;
using tourBD.Membership.Services;
using tourBD.Web.Models;
using tourBD.Web.Models.UserModel;

namespace tourBD.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICompanyService _companyService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            ILogger<AccountController> logger,
            IWebHostEnvironment environment,
            ICompanyService companyService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _webHostEnvironment = environment;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterAsync(string returnUrl = null)
        {
            var model = new RegisterModel() { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("RegistrationForm", user);
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            var model = new LoginModel();
            model.ReturnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Forum");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> RegistrationForm(IdentityUser user)
        {
            ViewBag.Name = user.Email;
            ViewBag.ImageUrl = @"\img\profile-no.png";
            ViewBag.UserEmail = user.Email;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationForm(RegistrationFormModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); // returns ApplicationUser
                string imagePath = @"\img\Upload\";
                string uploadPath = _webHostEnvironment.WebRootPath + imagePath;
                string demoImage = @"\img\profile-no.png";

                if (user != null)
                {
                    user.IsVarified = true;
                    user.FullName = model.Name;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Mobile;
                    user.Address = model.Address;
                    user.ImageUrl = await GeneralUtilityMethods.GetSavedImageUrlAsync(model.ImageFile, uploadPath, imagePath, demoImage);

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Profile(string userId)
        {
            var model = new UserProfileViewModel() 
            { 
                User = await _userManager.FindByIdAsync(userId), 
                Companies = (await _companyService.GetUserCompaniesAsync(new Guid(userId))).ToList()
            };

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        
    }
}
