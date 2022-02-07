using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotorContext.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectingString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectingString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectingString);
    }
}
