using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Department
    {

        public int Id { get; set; }// by convention =>Pk Identit(1,1) 


        [Required]
        public string Code { get; set; }//reference type

        [Required]
        [MaxLength(50)]

        public string Name { get; set; }
        
        public DateTime DateOfCreation { get; set; }


        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    
    }
}
