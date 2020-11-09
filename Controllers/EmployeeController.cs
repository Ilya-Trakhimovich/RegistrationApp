using BL.DTO;
using BL.Interfaces;
using BL.Services;
using RegistrationApp.EF.Context;
using RegistrationApp.ViewModels;
using RegistrationApp.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RegistrationApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _appContext;
        private readonly IServiceFactory _service;

        public EmployeeController()
        {
            _appContext = new AppDbContext();
            _service = new ServiceFactory();
        }

        public ActionResult Index()
        {
            var employeesDTO = _service.EmployeeService.GetItems().ToList();
            var employeeVMIndex = new EmployeeIndexVM();
            employeeVMIndex.Employees = new List<EmployeeVM>();
            employeeVMIndex.Companies = new List<CompanyVM>();            

            for (var i = 0; i < employeesDTO.Count; i++)
            {
                var employeeVM = new EmployeeVM();

                employeeVM.Id = employeesDTO[i].Id;
                employeeVM.Name = employeesDTO[i].Name;
                employeeVM.Surname = employeesDTO[i].Surname;

                var company = employeesDTO[i].Company;

                if (company != null)
                {
                    employeeVM.Company = new CompanyVM()
                    {
                        Id = employeesDTO[i].Company.Id,
                        CompanyName = employeesDTO[i].Company.CompanyName,
                        OrganizationForm = employeesDTO[i].Company.OrganizationForm
                    };                    
                }               

                employeeVMIndex.Employees.Add(employeeVM);

                if (!employeeVMIndex.Companies.Exists(c => c.Id == employeeVM.Company.Id))
                {
                    employeeVMIndex.Companies.Add(employeeVM.Company);
                }
            }

            return View(employeeVMIndex);
        }

        [HttpPost]
        public RedirectToRouteResult UpdateEmployee(EmployeeVMUpdate employeeVM)
        {
            var employeeDTO = new EmployeeDTO()
            {
                Id = employeeVM.Id,
                Name = employeeVM.Name,
                Surname = employeeVM.Surname,
                Patronymic = employeeVM.Patronymic,
                EmploymentDate = employeeVM.EmploymentDate ?? DateTime.Now,
                Position = employeeVM.Position,
                Company = new CompanyDTO() { CompanyName = employeeVM.Companies[0] }
            };

            _service.EmployeeService.Update(employeeDTO);

            return RedirectToAction("Index");
        }

        public ActionResult UpdateEmployee(int? id)
        {
            var companies = GetCompaniesNames();
            var employee = _service.EmployeeService.GetItem(id) ?? new EmployeeDTO() { Company = new CompanyDTO() };

            var employeeVM = new EmployeeVMUpdate()
            {
                Name = employee.Name,
                Surname = employee.Surname,
                Patronymic = employee.Patronymic,
                EmploymentDate = employee.EmploymentDate,
                Position = employee.Position,
                Companies = companies
            };

            return View(employeeVM);
        }

        public RedirectToRouteResult DeleteEmployee(int id)
        {
            _service.EmployeeService.Delete(id);

            return RedirectToAction("Index");
        }

        public PartialViewResult GetDetails(int? id)
        {
            var employee = _service.EmployeeService.GetItem(id);

            var employeeDetails = new EmployeeGetDetailsVM();

            employeeDetails.Name = employee.Name;
            employeeDetails.Surname = employee.Surname;
            employeeDetails.Patronymic = employee.Patronymic;
            employeeDetails.Position = employee.Position;
            employeeDetails.EmploymentDate = employee.EmploymentDate;

            if (employee.Company != null)
            {
                employeeDetails.Company = new CompanyVM() { Id = employee.Company.Id, CompanyName = employee.Company.CompanyName };
            }

            return PartialView(employee);
        }

        [HttpPost]
        public RedirectToRouteResult AddEmployee(EmployeeVMAdd employeeVM)
        {
            var employeeDTO = new EmployeeDTO();

            employeeDTO.Name = employeeVM.Name;
            employeeDTO.Surname = employeeVM.Surname;
            employeeDTO.Patronymic = employeeVM.Patronymic;
            employeeDTO.Position = employeeVM.Position;
            employeeDTO.EmploymentDate = employeeVM.EmploymentDate ?? DateTime.Now;
            employeeDTO.Company = new CompanyDTO() { CompanyName = employeeVM.Companies[0] };

            _service.EmployeeService.Create(employeeDTO);

            return RedirectToAction("Index");
        }

        public ActionResult AddEmployee()
        {
            var companies = GetCompaniesNames();
            var employeeVM = new EmployeeVMAdd()
            {
                Companies = companies,
                Company = new CompanyVM()
            };

            return View("AddEmployee", employeeVM);
        }

        private List<string> GetCompaniesNames()
        {
            var companiesNames = new List<string>();
            var employeesDTO = _service.EmployeeService.GetItems().ToList();

            for (var i = 0; i < employeesDTO.Count(); i++)
            {
                if (!companiesNames.Contains(employeesDTO[i].Company.CompanyName))
                    companiesNames.Add(employeesDTO[i].Company.CompanyName);
            }

            return companiesNames;
        }
    }
}