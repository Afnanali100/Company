using AutoMapper;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public IMapper Mapper { get; }

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.roleManager = roleManager;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index(string name)
        {



            if (string.IsNullOrEmpty(name))
            {
                var Roles = await roleManager.Roles.Select(r => new RoleViewModel()
                {
                    Id= r.Id,
                    RoleName= r.Name,

                }).ToListAsync();
                return View(Roles);
            }
            else
            {
                var Role = await roleManager.FindByNameAsync(name);
                var MapperRole = new RoleViewModel()
                {
                   Id=Role.Id,
                   RoleName=Role.Name
                };
                return View(new List<RoleViewModel>() { MapperRole });
            }

        }



        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(RoleViewModel RoleVM)
        {

            if (ModelState.IsValid)
            {
                var MappedRole =  Mapper.Map<RoleViewModel,IdentityRole>(RoleVM);
             await roleManager.CreateAsync(MappedRole);

               
                return RedirectToAction(nameof(Index));
            }

            else
                return View(RoleVM);
        }


        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var role = await roleManager.FindByIdAsync(id);
            var MappedRole = Mapper.Map<IdentityRole, RoleViewModel>(role);



            return View(viewname, MappedRole);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(string id)
        {

            //  ViewBag.Departments = departmentRepository.GetAll();
            return await Details(id, "edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel RoleVM)
        {
            if (id != RoleVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    var role= await roleManager.FindByIdAsync(id);
                    role.Name = RoleVM.RoleName;

                    await roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(RoleVM);


        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, RoleViewModel RoleVM)
        {
            if (id != RoleVM.Id)
                return BadRequest();
            try
            {

                var user = await roleManager.FindByIdAsync(id);
                await roleManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(RoleVM);

        }



    }
}
