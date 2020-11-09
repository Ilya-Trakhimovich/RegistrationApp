using BL.Interfaces;
using DAL.Abstract;
using DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ServiceFactory : IServiceFactory
    {
        IUnitOfWork uow = new UnitOfWork();

        public EmployeeService EmployeeService => new EmployeeService(uow);

        public CompanyService CompanyService => new CompanyService(uow);
    }
}
