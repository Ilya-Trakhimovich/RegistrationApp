using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationApp.ViewModels
{
    public class EmployeeGetDetailsVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EmploymentDate { get; set; }
        public string Position { get; set; }
        public CompanyVM Company { get; set; }
    }
}