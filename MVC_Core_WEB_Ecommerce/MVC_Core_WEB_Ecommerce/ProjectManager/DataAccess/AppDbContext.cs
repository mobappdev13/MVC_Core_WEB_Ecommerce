using Microsoft.EntityFrameworkCore;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            :base()
        {}

        
        public AppDbContext(DbContextOptions options)
        :base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
