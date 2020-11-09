using BL.DTO;
using BL.Interfaces;
using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CompanyService : IService<Company>
    {
        private IUnitOfWork _uow;

        public CompanyService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Create(Company item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Company GetItem(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanyDTO> GetItems()
        {
            var companies = _uow.CompRepository.Items.ToList();

            var companiesDTO = new List<CompanyDTO>();

            for (var i = 0; i < companies.Count; i++)
            {
                var company = new CompanyDTO()
                {
                    Id = companies[i].Id,
                    CompanyName = companies[i].CompanyName,
                    OrganizationForm = companies[i].OrganizationForm
                };

                companiesDTO.Add(company);
            }

            return companiesDTO;
        }

        public void Update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
