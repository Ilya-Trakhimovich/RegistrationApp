using RegistrationApp.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationApp.ViewModels
{
    public class EmployeeIndexVM
    {
        public List<EmployeeVM> Employees { get; set; }
        public List<CompanyVM> Companies { get; set; }
    }
}