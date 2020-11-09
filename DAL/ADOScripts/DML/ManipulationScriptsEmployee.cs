using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ADOScripts.DML
{
    public static class ManipulationScriptsEmployee
    {
        public static string sql_SELECT_IndexData_Employees = "SELECT E.Id as Id, E.Name, E.Surname, C.CompanyName, C.Id as CompanyId" +
                                                        " FROM Employees as E JOIN Companies as C " +
                                                        " ON E.CompanyId = C.Id";

        public static string Sql_INSERT_INTO_Employees(string companyName) =>
            $"INSERT INTO Employees (Name, Surname, Patronymic, EmploymentDate, Position, CompanyId)" +
            $" VALUES (@name, @surname, @patronymic, @empDate, @position, (SELECT C.Id FROM Companies as C WHERE C.CompanyName = '{companyName}'))";
        
        public static string Sql_DELETE_Employee(int id) => $"DELETE FROM Employees WHERE Id = {id}";

        public static string Sql_SELECT_SingleEmployee(int? id) =>
                $"SELECT E.Id as Id, E.Name, E.Surname, E.Patronymic, E.EmploymentDate, E.Position, C.CompanyName, C.Id as CompanyId" +
                $" FROM Employees as E" +
                $" JOIN Companies as C" +
                $" ON E.CompanyId = C.Id" +
                $" WHERE E.Id = {id}";

        public static string Sql_UPDATE_Employee(string companyName) => $"UPDATE Employees" +
            $" SET Name = @name, Surname = @surname, Patronymic = @patronymic, EmploymentDate = @empDate, Position = @position, " +
            $" CompanyId = (SELECT C.Id FROM Companies AS C WHERE C.CompanyName = '{companyName}')" +
            $" WHERE Id = @id";

    }
}

