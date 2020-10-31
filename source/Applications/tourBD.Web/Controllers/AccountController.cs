using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
        private readonly IPathService _pathService;
        private readonly IConfiguration _configuration;

        public AccountController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            ILogger<AccountController> logger,
            IWebHostEnvironment environment,
            ICompanyService companyService,
            IPathService pathService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _webHostEnvironment = environment;
            _companyService = companyService;
            _pathService = pathService;
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
            var model = new LoginModel
            {
                ReturnUrl = returnUrl ?? Url.Content("~/"),
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };


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
                    return RedirectToAction("Index", "Home");
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
            ViewBag.ImageUrl = $"{_pathService.PictureFolder}{_pathService.DummyUserImageUrl}";
            ViewBag.UserEmail = user.Email;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationForm(RegistrationFormModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                string imagePath = _pathService.PictureFolder;
                string uploadPath = _webHostEnvironment.WebRootPath + imagePath;
                string demoImage = _pathService.DummyUserImageUrl;

                if (user != null)
                {
                    user.IsVarified = true;
                    user.FullName = model.Name;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Mobile;
                    user.Address = model.Address;
                    user.ImageUrl = await GeneralUtilityMethods.GetSavedImageUrlAsync(model.ImageFile, uploadPath, demoImage);

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Profile(string userId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.ImageUrl = $"{_pathService.PictureFolder}{user.ImageUrl}";

            var model = new UserProfileViewModel() 
            {
                User = await _userManager.FindByIdAsync(userId), 
                Companies = (await _companyService.GetUserCompaniesAsync(new Guid(userId))).ToList()
            };

            if (!model.User.ImageUrl.Contains(_pathService.PictureFolder))
                model.User.ImageUrl = $"{_pathService.PictureFolder}{model.User.ImageUrl}";

            model.Companies.ForEach(c => c.CompanyImageUrl = $"{_pathService.PictureFolder}{c.CompanyImageUrl}");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.ImageUrl = _pathService.PictureFolder + user.ImageUrl;
            var model = new RegistrationFormModel()
            {
                Name = user.FullName,
                Email = user.Email,
                Mobile = user.PhoneNumber,
                Address = user.Address
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(RegistrationFormModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); // returns ApplicationUser
                string imagePath = _pathService.PictureFolder;
                string uploadPath = _webHostEnvironment.WebRootPath + imagePath;
                string demoImage = _pathService.DummyUserImageUrl;

                if (user != null)
                {
                    user.IsVarified = true;
                    user.FullName = model.Name;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Mobile;
                    user.Address = model.Address;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        user.ImageUrl = await GeneralUtilityMethods.GetSavedImageUrlAsync(model.ImageFile, uploadPath, demoImage);
                    }
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Profile", "Account", new { userId = user.Id.ToString() });
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl) // value is maped to provider here as name = "provider"
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties); // take us to google signin page.
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null) // Google/FB call this method.
        {
            LoginModel loginModel = new LoginModel
            {
                ReturnUrl = returnUrl ?? Url.Content("~/"),
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error from external provider: {remoteError}");
                return View("Join", loginModel);
            }

            var userInfo = await _signInManager.GetExternalLoginInfoAsync(); // Google gives userInfo
            if (userInfo == null)
            {
                ModelState.AddModelError("", "Error loading external login information");
                return View("Loin", loginModel);
            }

            var signinResult = await _signInManager.ExternalLoginSignInAsync(userInfo.LoginProvider, userInfo.ProviderKey,
                isPersistent: false, bypassTwoFactor: true); // Check AspNetUserLogin table to find corresponding entry to sign the user in.


            var email = userInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var name = userInfo.Principal.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByEmailAsync(email);

            if (signinResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (email != null) // user has local account?
                {
                    if (user == null) // no local user account found
                    {
                        user = new ApplicationUser { UserName = email, Email = email, FullName = name };
                        var result = await _userManager.CreateAsync(user); // Create new user to AspNetUsers table

                        if (!result.Succeeded)
                        {
                            ViewBag.ErrorTitle = $"Failed to create new user";
                            return View("Error");
                        }
                    }

                    await _userManager.AddLoginAsync(user, userInfo); // Add user entry to AspNetUserLogin table.
                    await _signInManager.SignInAsync(user, isPersistent: false); // sign the user in.

                    return RedirectToAction("RegistrationForm", user);
                }

                // If we do not receive email from [External Login Provider]
                ViewBag.ErrorTitle = $"Email claim not received from {userInfo.LoginProvider}";
                return View("Error");
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        
    }
}
