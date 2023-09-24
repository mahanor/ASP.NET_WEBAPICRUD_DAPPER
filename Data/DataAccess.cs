using Microsoft.Data.SqlClient;
using System.Data;

namespace Dapper_CrudWebApi.Data
{
    public class DataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionstring;
        public DataAccess(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connectionstring = _configuration.GetConnectionString("myconn");
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionstring);

    }
}
