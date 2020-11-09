using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationApp.ViewModels.Employee
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public CompanyVM Company { get; set; }
    }
}