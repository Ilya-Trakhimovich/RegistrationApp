using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationApp.EF.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string OrganizationForm { get; set; }
        public List<Employee> Employees { get; set; }
    }
}