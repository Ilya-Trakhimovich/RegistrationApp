using DAL.Abstract;
using DAL.Entities;
using System;
using System.Configuration;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;  
        private  EmployeeRepository _empRepository;
        private  CompanyRepository _compRepository;

        public UnitOfWork()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;
        }

        public IRepository<Company> CompRepository
        {
            get
            {
                _compRepository =  new CompanyRepository(_connectionString);

                return _compRepository;
            }
        }

        public IRepository<Employee> EmpRepository
        {
            get 
            {
                _empRepository = new EmployeeRepository(_connectionString);

                return _empRepository;
            }
        }       

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _empRepository.Dispose();
                    _compRepository.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}
