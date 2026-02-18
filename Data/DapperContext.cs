using Microsoft.Data.SqlClient;
using System.Data;

namespace BookStorePerfApi.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }
        public IDbConnection OpenConnection()
        {
            return new SqlConnection(this.connectionString);
        }


    }
}
