using Microsoft.EntityFrameworkCore;
using EMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Data
{
    public class UMSContext : DbContext
    {
        public UMSContext(DbContextOptions<UMSContext> options)
          : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Degree> Degrees { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}