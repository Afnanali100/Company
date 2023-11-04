﻿using Company.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Contexts
{
    public class CompanyDbContext:IdentityDbContext<ApplicationUser>
    {

        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {

        }
            
        

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("server=.;database=CompanyMVC;trusted_connection=true");




        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public  DbSet<IdentityUser> Users { get; set; }
        //public DbSet<IdentityRole> Roles { get; set; }
    }
}
