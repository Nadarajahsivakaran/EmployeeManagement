using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasData(
                new Employee { 
                    Id = 1,
                    Name = "karan",
                    Age = 20,
                    Address ="jaffna",
                },
                 new Employee
                 {
                     Id = 2,
                     Name = "mala",
                     Age = 25,
                     Address = "colombo",
                 }
               );

            base.OnModelCreating(builder);
        }
    }
}