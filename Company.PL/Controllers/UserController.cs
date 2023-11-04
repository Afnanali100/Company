using AutoMapper;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Company.BLL.Interfaces;

namespace Company.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

        public IMapper Mapper { get; }

        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
            Mapper = mapper;
        }
		public async Task<IActionResult> Index(string email)
		{
		


			if (string.IsNullOrEmpty(email))
			{
				var users = await userManager.Users.Select(u => new UserViewModel()
				{
					Id = u.Id,
					FName = u.FName,
					LName = u.LName,
					Email = u.Email,
					PhoneNumber = u.PhoneNumber,
					Roles = userManager.GetRolesAsync(u).Result
				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await userManager.FindByEmailAsync(email);
				var MapperUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel>() { MapperUser });
			}	
			
		}


        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

			var user = await userManager.FindByIdAsync(id);
            var MappedUser = Mapper.Map<ApplicationUser, UserViewModel>(user);

           

            return View(viewname, MappedUser);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(string id)
        {

            //  ViewBag.Departments = departmentRepository.GetAll();
            return await Details(id, "edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel UserVM)
        {
            if (id != UserVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    var user = await userManager.FindByIdAsync(id);
                    user.FName = UserVM.FName;
                    user.LName = UserVM.LName;
                    user.PhoneNumber = UserVM.PhoneNumber;
               
                    await userManager.UpdateAsync(user);
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(UserVM);


        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, UserViewModel UserVM)
        {
            if (id != UserVM.Id)
                return BadRequest();
            try
            {

               var user=await userManager.FindByIdAsync(id);
              await userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(UserVM);

        }



    }
}
