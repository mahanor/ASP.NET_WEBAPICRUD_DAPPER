using Dapper_CrudWebApi.Models;
using static Dapper_CrudWebApi.Repo.IEmployeeRepo;

namespace Dapper_CrudWebApi.Repo
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAllEmp();
        Task<string>Insert(Employee emp);
        Task<string> Update(Employee emp);
        Task<string> Delete(int EmpId);

    }
}
