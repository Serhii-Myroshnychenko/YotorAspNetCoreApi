using Dapper;
using System.Data;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorContext.Context;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class HelpRepository : IHelpRepository
    {
        private readonly DapperContext _dapperContext;
        public HelpRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<Landlord> IsLandlordAsync(int id)
        {
            var query = "Select * from Landlord Where user_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Landlord>(query, new { id });
            }
        }
        public async Task<bool> IsAdminAsync(int id)
        {
            var query = "Select * from Customer Where user_id = @id and is_admin = 1";
            using (var connection = _dapperContext.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Customer>(query, new { id });
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> IsUserAsync(int id)
        {
            var query = "Select * from Customer Where user_id = @id";
            using(var connection = _dapperContext.CreateConnection())
            {
                var customer = await connection.QueryFirstOrDefaultAsync<Customer>(query, new { id });
                if (customer != null)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> IsOrganizationAsync(int id)
        {
            var query = "Select * from Organization Where organization_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var organization = await connection.QueryFirstOrDefaultAsync<Organization>(query, new { id });
                if (organization != null)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<Landlord> IsThisCarOfHisOrganizationAsync(string name)
        {
            var query = "select Landlord.landlord_id,Landlord.user_id,Landlord.organization_id,Landlord.name from Car join Landlord ON Car.organization_id =  Landlord.organization_id Where Car.model = @name";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Landlord>(query, new { name});
            }
        }
        public async Task<Restriction> GetRestrictionByCarNameAsync(string name)
        {
            var query = "Select * from Restriction Where car_name = @name";
            using(var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Restriction>(query, new { name });
            }
        }
        public async Task<Car> GetCarByCarNameAsync(string name)
        {
            bool stat = true;
            var query = "Select * from Car Where model = @name and status = @stat";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Car>(query, new { name,stat });
            }
        }
        public async Task UpdateStatusCarAsync(int id)
        {
            var query = "UPDATE Car SET status = @status WHERE car_id = @id";
            
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("status", false, DbType.Boolean);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<Customer> GetCustomerByNameAsync(string name)
        {
            var query = "select * from Customer where full_name = @name";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { name });
            }
        }
        public async Task<Organization> GetOrganizationByNameAsync(string name)
        {
            var query = "select * from Organization where name = @name";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Organization>(query, new { name });
            }
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var query = "Select * from Customer Where user_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Customer>(query, new { id });
            }
        }
    }
}
