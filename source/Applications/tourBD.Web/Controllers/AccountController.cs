using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using tourBD.Membership.Entities;
using tourBD.Web.Models;

namespace tourBD.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            ILogger<AccountController> logger,
            IWebHostEnvironment environment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _webHostEnvironment = environment;
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
            ViewBag.ImageUrl = @"\img\no-profile.png";
            ViewBag.UserEmail = user.Email;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationForm(RegistrationFormModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); // returns ApplicationUser
                if (user != null)
                {
                    user.IsVarified = true;
                    user.FullName = model.Name;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Mobile;
                    user.Address = model.Address;
                    user.ImageUrl = await GetSavedImageUrlAsync(model.ImageFile);

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<string> GetSavedImageUrlAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\img\Upload\";
                var uploadPath = _webHostEnvironment.WebRootPath + imagePath;

                // Create directory
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Create unique file name
                var guid = "pic" + Guid.NewGuid().ToString();
                var uniqueFileName = Path.Combine(guid + "." + file.FileName.Split(".")[1].ToLower());
                var fullPath = uploadPath + uniqueFileName;
                var imageVirtualPath = imagePath + uniqueFileName;

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return imageVirtualPath;
            }
            else
                return @"\img\no-profile.png";
        }
    }
}
