using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    
    public class EmployeeRepository :GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly CompanyDbContext dbContext;

        public EmployeeRepository(CompanyDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return dbContext.Employees.Where(e => e.Address == address);
        }

        public IQueryable<Employee> GetEmployeesByName(string Name)
        
            => dbContext.Employees.Where(e=>e.Name.ToLower().Contains(Name.ToLower()));

      
    }
}
