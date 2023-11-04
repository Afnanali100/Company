using Company.BLL.Interfaces;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Mocks
{
    internal class MockEmployeeRepository : IEmployeeRepository
    {
        public void Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Delete(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> GetEmployeesByName(string Name)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<Employee>.Add(Employee item)
        {
            throw new NotImplementedException();
        }

        Task<Employee> IGenericRepository<Employee>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Employee>> IGenericRepository<Employee>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
