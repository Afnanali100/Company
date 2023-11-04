using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; set; }
 
        IDepartmentRepository departmentRepository { get; set; }
        public Task<int> Complete();
    
    }
}
