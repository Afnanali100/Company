using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee
    {

        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
  
        public string Name { get; set; }

        [Required]
      
        public int Age { get; set; }

      
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        
        public double Salary { get; set; }

        public bool IsActive { get; set; }

    
        [Required]
        public string Email { get; set; }

       

        [Required]
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is Required !!")]

        public DateTime HireDate { get; set; } = DateTime.Now;


        public string ImageName { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreationDate { get; set; }

        //FK required=> on delete:cascade
        //FK Optional =>on Delete:Restrict

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
       
      
        public Department Department { get; set; }
    }
}
