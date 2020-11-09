using DAL.Abstract;
using DAL.ADOScripts.DML;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DAL.Concrete
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        IEnumerable<Employee> IRepository<Employee>.Items => Items();

        public IEnumerable<Employee> Items()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(ManipulationScriptsEmployee.sql_SELECT_IndexData_Employees, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.Id = reader.GetInt32(0);
                        employee.Name = reader.GetString(1);
                        employee.Surname = reader.GetString(2);

                        var companyId = reader.GetInt32(4);

                        if (companyId != default)
                        {
                            var companyName = reader.GetString(3);
                            employee.Company = new Company() { CompanyName = companyName, Id = companyId };
                        }

                        employees.Add(employee);
                    }
                }

                reader.Close();
            }

            return employees;
        }

        public void Create(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(ManipulationScriptsEmployee.Sql_INSERT_INTO_Employees(employee.Company.CompanyName), connection);

                command.Parameters.AddWithValue("@position", employee.Position);
                command.Parameters.AddWithValue("@empDate", employee.EmploymentDate);
                command.Parameters.AddWithValue("@patronymic", employee.Patronymic);
                command.Parameters.AddWithValue("@surname", employee.Surname);
                command.Parameters.AddWithValue("@name", employee.Name);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteItem(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(ManipulationScriptsEmployee.Sql_DELETE_Employee(id), connection);

                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Employee GetItem(int? id)
        {
            var employee = new Employee();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(ManipulationScriptsEmployee.Sql_SELECT_SingleEmployee(id), connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.Id = reader.GetInt32(0);
                        employee.Name = reader.GetString(1);
                        employee.Surname = reader.GetString(2);
                        employee.Patronymic = reader.GetString(3);
                        employee.EmploymentDate = reader.GetDateTime(4);
                        employee.Position = reader.GetString(5);
                        var companyId = reader.GetInt32(7);

                        if (companyId != default)
                        {
                            var companyName = reader.GetString(6);
                            employee.Company = new Company() { CompanyName = companyName, Id = companyId };
                        }
                    }
                }

                reader.Close();
            }

            return employee;
        }

        public void Update(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(ManipulationScriptsEmployee.Sql_UPDATE_Employee(employee.Company.CompanyName), connection);

                command.Parameters.AddWithValue("@position", employee.Position);
                command.Parameters.AddWithValue("@empDate", employee.EmploymentDate);
                command.Parameters.AddWithValue("@patronymic", employee.Patronymic);
                command.Parameters.AddWithValue("@surname", employee.Surname);
                command.Parameters.AddWithValue("@name", employee.Name);
                command.Parameters.AddWithValue("@id", employee.Id);

                command.ExecuteNonQuery();
            }
        }        
    }
}
