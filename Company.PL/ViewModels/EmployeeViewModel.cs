using Company.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Company.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is Required !!")]
        [MaxLength(50, ErrorMessage = "Max Lenghth is 50 chars")]
        [MinLength(5, ErrorMessage = "Min Lenghth is 5 chars")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is Required !!")]
        [Range(20, 50, ErrorMessage = "Age Must Be In Range From 20 to 50")]
        public int Age { get; set; }

        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Adreess Must Be" +
            "Like 123-Street-City-Country")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary is Required !!")]
        [DataType(DataType.Currency)]

        public double Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required !!")]
        public string Email { get; set; }

        [Phone]

        [Required(ErrorMessage = "phone number is Required !!")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is Required !!")]

        public DateTime HireDate { get; set; } = DateTime.Now;

        public string ImageName { get; set; }
        public IFormFile Image { get; set; }


        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is Required !!")]
        public DateTime CreationDate { get; set; }

        //FK required=> on delete:cascade
        //FK Optional =>on Delete:Restrict

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }


        public Department Department { get; set; }
    }
}
