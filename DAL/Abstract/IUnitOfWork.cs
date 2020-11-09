using DAL.Concrete;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Company> CompRepository { get; }
        IRepository<Employee> EmpRepository { get; }
    }
}
