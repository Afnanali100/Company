using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext dbcontext;
        public GenericRepository(CompanyDbContext dbcontext)
        {
            this.dbcontext = dbcontext; ;
        }



        public async Task Add(T item)
        {


           await dbcontext.Set<T>().AddAsync(item);
          

        }

        public void Delete(T item)
        {
            dbcontext.Set<T>().Remove(item);
           
        }

        public async Task<T> Get(int id)

           => await dbcontext.Set<T>().FindAsync(id);


        public async Task<IEnumerable<T>> GetAll()

        {//Untill Knowing Specification Pattern
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await dbcontext.Employees.Include(e => e.Department).ToListAsync();
            }
            return await dbcontext.Set<T>().ToListAsync();
        }

        public void Update(T item)
        {
            dbcontext.Set<T>().Update(item);
          
        }
    }
}
