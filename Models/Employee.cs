namespace Dapper_CrudWebApi.Models
{
    public class Employee
    {
      
        public int EmpId { get; set; } 
        public string EmpName { get; set; }

        public string EmpEmail { get; set; }
        public string EmpPassword { get; set; }

        public string EmpAddress { get; set; }
    }
}
