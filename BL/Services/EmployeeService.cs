using BL.DTO;
using BL.Interfaces;
using DAL.Abstract;
using DAL.ADOScripts.DML;
using DAL.Concrete;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class EmployeeService : IService<EmployeeDTO>
    {
        private readonly IUnitOfWork _uow;
        public List<string> CompanyNames { get; set; }

        public EmployeeService(IUnitOfWork uow)
        {
            _uow = uow;
            CompanyNames = new List<string>();
        }

        public IEnumerable<EmployeeDTO> GetItems()
        {
            var employees = _uow.EmpRepository.Items;

            List<EmployeeDTO> empList = new List<EmployeeDTO>();

            foreach (var e in employees)
            {
                EmployeeDTO employeeDTO = new EmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Surname = e.Surname
                };

                if (e.Company != null)
                {
                    employeeDTO.Company = new CompanyDTO() { Id = e.Company.Id, CompanyName = e.Company.CompanyName };

                    if (CompanyNames.Contains(e.Company.CompanyName))
                        CompanyNames.Add(e.Company.CompanyName);
                }

                empList.Add(employeeDTO);
            }

            return empList;
        }

        public void Create(EmployeeDTO employeeDTO)
        {
            Employee employee = new Employee
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                Surname = employeeDTO.Surname,
                Patronymic = employeeDTO.Patronymic,
                EmploymentDate = employeeDTO.EmploymentDate,
                Position = employeeDTO.Position,
                Company = new Company() { CompanyName = employeeDTO.Company.CompanyName }
            };

            _uow.EmpRepository.Create(employee);
        }

        public void Update(EmployeeDTO employeeDTO)
        {
            var employee = new Employee()
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                Surname = employeeDTO.Surname,
                Patronymic = employeeDTO.Patronymic,
                EmploymentDate = employeeDTO.EmploymentDate,
                Position = employeeDTO.Position,
                Company = new Company() { CompanyName = employeeDTO.Company.CompanyName}
            };

            _uow.EmpRepository.Update(employee);
        }

        public void Delete(int id)
        {
            _uow.EmpRepository.DeleteItem(id);
        }

        public EmployeeDTO GetItem(int? id)
        {
            var employee = _uow.EmpRepository.GetItem(id);

            var employeeDTO = new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Position = employee.Position,
                EmploymentDate = employee.EmploymentDate,
                Patronymic = employee.Patronymic
            };

            if (employee.Company != null)
            {
                employeeDTO.Company = new CompanyDTO() { Id = employee.Company.Id, CompanyName = employee.Company.CompanyName };
            }

            return employeeDTO;
        }
    }
}