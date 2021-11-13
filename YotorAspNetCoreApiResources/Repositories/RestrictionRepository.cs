using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Context;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class RestrictionRepository : IRestrictionRepository
    {
        private readonly DapperContext _dapperContext;
        public RestrictionRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task CreateRestriction(int landlord_id, string car_name, string description)
        {
            var query = "INSERT INTO Restriction (landlord_id, car_name, description) values (@landlord_id, @car_name, @description);";
            var parameters = new DynamicParameters();
            parameters.Add("landlord_id", landlord_id, DbType.Int64);
            parameters.Add("car_name", car_name, DbType.String);
            parameters.Add("description", description, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteRestriction(int id)
        {
            var query = "Delete from Restriction where restriction_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.QuerySingleOrDefaultAsync<Restriction>(query, new { id });
            }
        }

        public async Task<Restriction> GetRestriction(int id)
        {
            var query = "SELECT * FROM Restriction Where restriction_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var restriction = await connection.QuerySingleOrDefaultAsync<Restriction>(query, new { id });
                return restriction;
            }
        }

        public async Task<IEnumerable<Restriction>> GetRestrictions()
        {
            var query = "SELECT * FROM Restriction";
            using (var connection = _dapperContext.CreateConnection())
            {
                var restrictions = await connection.QueryAsync<Restriction>(query);
                return restrictions.ToList();
            }
        }

        public async Task UpdateRestriction(int id, Restriction restriction)
        {
            var query = "UPDATE Restriction SET landlord_id = @landlord_id, car_name = @car_name, description = @description WHERE restriction_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);
            parameters.Add("landlord_id", restriction.Landlord_id, DbType.Int64);
            parameters.Add("car_name", restriction.Car_name, DbType.String);
            parameters.Add("descriptoin", restriction.Description, DbType.String);


            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
