using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Company.DAL.Models;
using System.Threading.Tasks;
using System;
using Company.PL.Helpers;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;

		public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
			this.userManager = userManager;
			SignInManager = signInManager;
		}

		public SignInManager<ApplicationUser> SignInManager { get; }

		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model) {
        if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree
                };
                var result=await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
        return View(model);
        
        
        }
        [HttpGet]
		public IActionResult Login()
		{
            return View();
		}
        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null) {
                    var flag = await userManager.CheckPasswordAsync(user, model.Password);

                    if (flag)
                    {
                        var result = await SignInManager.PasswordSignInAsync(user, model.Password,model.RememberMe,false);
                        if(result.Succeeded)
                            return RedirectToAction("Index", "Home");
                        

					}
                    ModelState.AddModelError(string.Empty, "Password is invalid");

                }
                ModelState.AddModelError(string.Empty, "Email Is Already InValid");
     
            } 
			return View(model);
		}

	 public new async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
         public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendeEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user=await userManager.FindByEmailAsync(model.Email);   
               if(user is not null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email,token=Token },"https",Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To=user.Email,
                        Body= PasswordResetLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email is Not Vaild");
            
            }
            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"]= token;

            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ResetPassword( ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string Email = TempData["email"] as string;
				string Token = TempData["token"] as string;
                var user=await userManager.FindByEmailAsync(Email);
                var result = await userManager.ResetPasswordAsync(user, Token, model.NewPassword);
                if (result.Succeeded)
                {
                    RedirectToAction(nameof(Login));
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            
            }
            return View(model);
    
        }
		

	}



}
