using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Context;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;
using System.Data;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {
        private readonly DapperContext _dapperContext;
        public LandlordRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task CreateLandlordAsync(int user_id, int organization_id, string name)
        {
            var query = "INSERT INTO Landlord (user_id, organization_id, name) values (@user_id, @organization_id, @name);";
            var parameters = new DynamicParameters();
            parameters.Add("user_id", user_id, DbType.Int64);
            parameters.Add("organization_id", organization_id, DbType.Int64);
            parameters.Add("name", name, DbType.String);



            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Landlord> GetLandlordAsync(int id)
        {
            var query = "Select * from Landlord where landlord_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var landlord = await connection.QuerySingleOrDefaultAsync<Landlord>(query, new { id});
                return landlord;
            }
        }

        public async Task<IEnumerable<Landlord>> GetLandlordsAsync()
        {
            var query = "Select * from Landlord";
            using(var connection = _dapperContext.CreateConnection())
            {
                var landlords = await connection.QueryAsync<Landlord>(query);
                return landlords.ToList();
            }
        }

        public async Task UpdateLandlordAsync(int id, Landlord landlord)
        {
            var query = "UPDATE Landlord SET user_id = @user_id, organization_id = @organization_id, name = @name WHERE landlord_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);
            parameters.Add("user_id", landlord.User_id, DbType.Int64);
            parameters.Add("organization_id", landlord.Organization_id, DbType.Int64);
            parameters.Add("name", landlord.Name, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
