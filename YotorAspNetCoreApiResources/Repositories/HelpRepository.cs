using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Context;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class HelpRepository : IHelpRepository
    {
        private readonly DapperContext _dapperContext;
        public HelpRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<Landlord> IsLandlord(int id)
        {
            var query = "Select * from Landlord Where user_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var landlord = await connection.QuerySingleOrDefaultAsync<Landlord>(query, new { id });
                return landlord;
            }
        }
        public async Task<bool> IsAdmin(int id)
        {
            var query = "Select * from Customer Where user_id = @id and is_admin = 1";
            using (var connection = _dapperContext.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Landlord>(query, new { id });
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
