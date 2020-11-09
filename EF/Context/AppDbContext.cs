using RegistrationApp.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RegistrationApp.EF.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

        static AppDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            context.Employees.Add(new Employee() { Name = "Oleg", Surname = "Ivanov", Patronymic = "Ivanov", EmploymentDate = new DateTime(2000, 4,18), Position = "Rec", CompanyId = 1 });
            context.Employees.Add(new Employee() { Name = "Ivan", Surname = "Olegovich", Patronymic = "Adamovich", EmploymentDate = new DateTime(2013, 6, 18), Position = "Dev", CompanyId = 2 });
            context.Employees.Add(new Employee() { Name = "Olga", Surname = "Bird", Patronymic = "Sergeevna", EmploymentDate = new DateTime(2000, 3, 21), Position = "Dev", CompanyId = 3 });
            context.Employees.Add(new Employee() { Name = "Ignat", Surname = "Petrov", Patronymic = "Petrovich", EmploymentDate = new DateTime(2000, 12, 1), Position = "Dev", CompanyId = 2 });
            context.Companies.Add(new Company() { CompanyName = "Puma", OrganizationForm = "OOO" });
            context.Companies.Add(new Company() { CompanyName = "Adidas", OrganizationForm = "OOO" });
            context.Companies.Add(new Company() { CompanyName = "Nike", OrganizationForm = "OOO" });
            context.SaveChanges();
        }
    }
}