using Company.BLL.Interfaces;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Mocks
{
    internal class MocDepartmentRepository : IDepartmentRepository
    {
       
        public void Add(Department department)
        {
            throw new NotImplementedException();
        }

        public void Delete(Department department)
        {
            throw new NotImplementedException();
        }

        public Department Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Department> GetDepartmentByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Department department)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<Department>.Add(Department item)
        {
            throw new NotImplementedException();
        }

        Task<Department> IGenericRepository<Department>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Department>> IGenericRepository<Department>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
