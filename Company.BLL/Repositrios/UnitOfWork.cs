using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class UnitOfWork : IUnitOfWork
    {
        private  readonly CompanyDbContext dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository departmentRepository { get; set; }

        public UnitOfWork(CompanyDbContext dbContext)
        {
           
                EmployeeRepository = new EmployeeRepository(dbContext);
                departmentRepository = new DepartmentRepository(dbContext);
                this.dbContext = dbContext;
        }
   

        public async Task<int> Complete()
        {
          return await dbContext.SaveChangesAsync();
        }
      

       public void Dispose()
        {
          dbContext.Dispose();
        }
    }
}
