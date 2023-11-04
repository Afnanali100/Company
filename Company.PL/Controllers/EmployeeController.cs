using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        // private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

//        public IDepartmentRepository departmentRepository { get; }

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
           // this.employeeRepository = employeeRepository;
           //this.departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            #region ViewData
            //1.ViewData =>KeyValuePair[Dictionary object]
            //transefer data from controller[Action] to Its View
            // .Net Framework 3.5
            //transefer data from view to layout
             ViewData["Message"] = "Hello From View Data";
            //Enforce type safty ->Require casting from IEnumrable to string[as string] because it is strongly typed
            string VDataMessage = ViewData["Message"] as string;
            //faster from ViewBag as it defines its data type at firt not in the runtime
            #endregion

            #region ViewBag
            //2.ViewBag =>Dynamic Property [Based on Dynamic KeyWord]
            //transefer data from Controller[Action] to its view
            //transefer data from view to layout
            //.net framework 4.0
            ViewBag.Message = "Hello From View Bag";
            //can't enforce type safty as depends on Dynamic which defines data type at runtime as it is weakly Typed
            string VBagMessage = ViewBag.Message;

            #endregion

            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees =await unitOfWork.EmployeeRepository.GetAll();
            else
                employees = unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
            
            var MappedEmp = mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Departments = departmentRepository.GetAll();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(EmployeeViewModel EmployeeVM)
        {
            
            if (ModelState.IsValid)
            {
               var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                if (EmployeeVM.Image is not null)
                {
                    MappedEmp.ImageName = DocumentSettings.UploadImage(EmployeeVM.Image, "Images");
                }

               await unitOfWork.EmployeeRepository.Add(MappedEmp);
                int result= await  unitOfWork.Complete();


                if (result > 0)
                {
                    #region TempData
                    //3.TempData=> Dictionary Object
                    //transefer data from Action To Action
                    //work with request when u make refresh it disappered

                    #endregion
                    TempData["Message"] = "Employee Is Created";
                }
                
                return RedirectToAction(nameof(Index));
            }

            else
                return View(EmployeeVM);
        }


        public async Task<IActionResult> Details(int? id,string viewname="Details")
        {
            if (id is null)
                return  BadRequest();
            
            var employee = await unitOfWork.EmployeeRepository.Get(id.Value);
            var MappedEmp=mapper.Map<Employee,EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();
            
            return View(viewname, MappedEmp);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
           
          //  ViewBag.Departments = departmentRepository.GetAll();
            return await Details(id, "edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,EmployeeViewModel EmployeeVM)
        {
            if(id != EmployeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp=mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                    MappedEmp.ImageName=DocumentSettings.UpdateImage(MappedEmp.ImageName, EmployeeVM.Image,"Images");
                    unitOfWork.EmployeeRepository.Update(MappedEmp);
                   await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(EmployeeVM);


        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id , EmployeeViewModel EmplployeeVM)
        {
            if(id != EmplployeeVM.Id)
                return BadRequest();
            try
            {
                
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmplployeeVM);
                if (MappedEmp.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(MappedEmp.ImageName, "Images");
                }


                    unitOfWork.EmployeeRepository.Delete(MappedEmp);
               await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View(EmplployeeVM);

        }
        




    }
}
