using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }

        //private readonly IDepartmentRepository departmentRepository;

        public IMapper Mapper { get; }

        public DepartmentController( IUnitOfWork unitOfWork,IMapper mapper)
        {
           // departmentRepository = new DepartmentRepository();   
         // this.departmentRepository = departmentRepository;
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {//GetAll()
            IEnumerable<Department> Departments;
            if (string.IsNullOrEmpty(SearchValue))
                Departments = await UnitOfWork.departmentRepository.GetAll();
            else
                Departments = UnitOfWork.departmentRepository.GetDepartmentByName(SearchValue);

            var MappedDeps = Mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(Departments);
            return View(MappedDeps);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel DepartmentVM)
        {
            if(ModelState.IsValid) //server side validation
            {
                var MappedDep = Mapper.Map<DepartmentViewModel, Department>(DepartmentVM);
               await UnitOfWork.departmentRepository.Add(MappedDep);
               await UnitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
          return View(DepartmentVM);
        }

        public async Task<IActionResult> Details(int? id,string viewName="Details")
        {
            if(id is null)
            {
                return BadRequest();
            }
            var department =await UnitOfWork.departmentRepository.Get(id.Value);
           
            if (department is null)
            {
                return NotFound();
            }
            var mappedDep = Mapper.Map<Department, DepartmentViewModel>(department);
            return View(viewName, mappedDep);
           
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if(id is null)
            //    return BadRequest();
            //var department=departmentRepository.Get(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
        return await Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, DepartmentViewModel DepartmentVM)
        {
            if(id != DepartmentVM.Id)
                return BadRequest();

            if(ModelState.IsValid)
            {
                try
                {
                    var mappedDep=Mapper.Map<DepartmentViewModel,Department>(DepartmentVM);
                    UnitOfWork.departmentRepository.Update(mappedDep);
                  await UnitOfWork.Complete();
                    return RedirectToAction(nameof (Index));
                }
                catch(Exception ex)
                {
                    //1. log exception
                    //2. friendly message
                    ModelState.AddModelError(string.Empty, ex.Message);
                   
                }
                
            }
            return View(DepartmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id,"Delete");
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,DepartmentViewModel DepartmentVM)
        {
            if (id != DepartmentVM.Id)
                return BadRequest();
            try
            {
                var mappedDep = Mapper.Map<DepartmentViewModel, Department>(DepartmentVM);
                UnitOfWork.departmentRepository.Delete(mappedDep);
               await UnitOfWork.Complete();
                return RedirectToAction(nameof(Index)); 
            }catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(DepartmentVM);
        } 
    }
}
