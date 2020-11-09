using DAL.Abstract;
using DAL.ADOScripts.DML;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly string _connectionString;

        IEnumerable<Company> IRepository<Company>.Items => Items();

        public CompanyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Company> Items()
        {
            List<Company> companies = new List<Company>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(ManipulationScriptsCompany.sql_SELECT_IndexData_Companies, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Company company = new Company
                        {
                            Id = reader.GetInt32(0),
                            CompanyName = reader.GetString(1),
                            OrganizationForm = reader.GetString(2)
                        };

                        companies.Add(company);
                    }
                }

                reader.Close();
            }

            return companies;
        }

        public void Create(Company item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Company GetItem(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Company item)
        {
            throw new NotImplementedException();
        }
    }
}
