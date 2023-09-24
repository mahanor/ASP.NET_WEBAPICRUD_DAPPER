using Dapper_CrudWebApi.Data;
using Dapper_CrudWebApi.Models;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Dapper_CrudWebApi.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DataAccess _DbAccess;

        public EmployeeRepo(DataAccess dbAccess)
        {
            _DbAccess = dbAccess;
        }

        ////GetAllEmp Using Query

        //public async Task<List<Employee>> GetAllEmp()
        //{
        //    string query = "Select * from Employee";
        //    using (var connection = this._DbAccess.CreateConnection())
        //    {
        //        var emplist = await connection.QueryAsync<Employee>(query);
        //        return emplist.ToList();
        //    }
        //}


        //// GetAllEmp using stored procedure
        public async Task<List<Employee>> GetAllEmp()
        {
            using (var connection = this._DbAccess.CreateConnection())
            {
                var emplist = await connection.QueryAsync<Employee>("GetAllEmp", commandType: CommandType.StoredProcedure);
                return emplist.ToList();
            }
        }


        //public async Task<string> Insert(Employee employee)
        //{
        //    string response = string.Empty;
        //    string query = "Insert into Employee(EmpId,EmpName,EmpEmail,EmpPassword,EmpAddress) values(@EmpId,@EmpName,@EmpEmail,@EmpPassword,@EmpAddress)";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("EmpId", employee.EmpId, DbType.String);
        //    parameters.Add("EmpName", employee.EmpName, DbType.String);
        //    parameters.Add("EmpEmail", employee.EmpEmail, DbType.String);
        //    parameters.Add("EmpPassword", employee.EmpPassword, DbType.String);
        //    parameters.Add("EmpAddress", employee.EmpAddress, DbType.String);
        //    using (var connection = this._DbAccess.CreateConnection())
        //    {
        //        await connection.ExecuteAsync(query, parameters);
        //        response = "Employee Added Successfully";

        //    }
        //    return response;
        //}

        //// Insert using stored procedure
        public async Task<string> Insert(Employee employee)
        {
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("EmpId", employee.EmpId, DbType.String);
            parameters.Add("EmpName", employee.EmpName, DbType.String);
            parameters.Add("EmpEmail", employee.EmpEmail, DbType.String);
            parameters.Add("EmpPassword", employee.EmpPassword, DbType.String);
            parameters.Add("EmpAddress", employee.EmpAddress, DbType.String);

            using (var connection = this._DbAccess.CreateConnection())
            {
                await connection.ExecuteAsync("InsertEmp", parameters, commandType: CommandType.StoredProcedure);
                response = "Employee Added Successfully";
            }

            return response;
        }


        public async Task<string> Update(Employee emp)
        {
            string response = string.Empty;
            string query = "Update Employee set EmpName=@EmpName,EmpEmail=@EmpEmail,EmpPassword=@EmpPassword,EmpAddress=@EmpAddress where EmpId=@EmpId";
            var parameters = new DynamicParameters();
            parameters.Add("EmpId", emp.EmpId, DbType.String);
            parameters.Add("EmpName", emp.EmpName, DbType.String);
            parameters.Add("EmpEmail", emp.EmpEmail, DbType.String);
            parameters.Add("EmpPassword", emp.EmpPassword, DbType.String);
            parameters.Add("EmpAddress", emp.EmpAddress, DbType.String);
            using (var connection = this._DbAccess.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                response = "Employee Updated Successfully";

            }
            return response;
        }

        public async Task<string> Delete(int EmpId)
        {
            string response = string.Empty;
            string query = "Delete from Employee where EmpId=@EmpId";
            using (var connection = this._DbAccess.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { EmpId });
                response = "Employee Deleted Successfully";

            }
            return response;
        }

        /*     public async Task<Employee> GetById(int EmployeeId)
             {
                 string response= string.Empty;
                 string query = "Select * from Employee where EmpId=@EmployeeId ";

                 using(var connection = this._DbAccess.CreateConnection())
                 {
                     await connection.ExecuteAsync(query,new { EmployeeId });
                     response = "eMP WITH ID ";
                 }


             }*/



        public async Task<Employee> GetById(int EmployeeId)
        {
            string query = "SELECT * FROM Employee WHERE EmpId = @EmployeeId";

            using (var connection = this._DbAccess.CreateConnection())
            {
                // Use QueryFirstOrDefaultAsync to retrieve the employee data
                // based on the provided EmployeeId.
                var employee = await connection.QueryFirstOrDefaultAsync<Employee>(query, new { EmployeeId });

                return employee;
            }
        }

    }
}
