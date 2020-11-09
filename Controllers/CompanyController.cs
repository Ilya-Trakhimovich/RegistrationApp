using BL.Interfaces;
using BL.Services;
using RegistrationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationApp.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IServiceFactory _service;

        public CompanyController()
        {
            _service = new ServiceFactory();
        }

        public ActionResult Index()
        {
            var companiesDTO = _service.CompanyService.GetItems().ToList();

            var companiesVM = new List<CompanyVM>();

            for (var i = 0; i < companiesDTO.Count; i++)
            {
                var companyVM = new CompanyVM()
                {
                    Id = companiesDTO[i].Id,
                    CompanyName = companiesDTO[i].CompanyName,
                    OrganizationForm = companiesDTO[i].OrganizationForm
                };

                companiesVM.Add(companyVM);
            }

            return View();
        }
    }
}