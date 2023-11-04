﻿using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class DepartmentRepository :GenericRepository<Department>,IDepartmentRepository
    {
        private readonly CompanyDbContext dbContext;

        public DepartmentRepository(CompanyDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Department> GetDepartmentByName(string name)
        
        => dbContext.Departments.Where(D=>D.Name.ToLower().Contains(name.ToLower()));   
        
    }
}
