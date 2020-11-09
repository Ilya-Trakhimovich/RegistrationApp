using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IServiceFactory
    {
        EmployeeService EmployeeService { get; }
        CompanyService CompanyService { get; }
    }
}
