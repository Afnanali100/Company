using Company.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Company.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }// by convention =>Pk Identit(1,1) 


        [Required(ErrorMessage = "Code is required !!")]
        public string Code { get; set; }//reference type

        [Required(ErrorMessage = "Name is Required !!")]
        [MaxLength(50)]

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }


        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
