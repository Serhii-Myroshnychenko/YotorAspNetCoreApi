using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorContext.Context;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DapperContext _dapperContext;
        public OrganizationRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task CreateOrganizationAsync(Organization organization)
        {
            var query = "INSERT INTO Organization (name,email,phone,code,taxes,address,founder,account) values (@name,@email,@phone,@code,@taxes,@address,@founder,@account);";
            
            var parameters = new DynamicParameters();
            parameters.Add("name", organization.Name, DbType.String);
            parameters.Add("email", organization.Email, DbType.String);
            parameters.Add("phone", organization.Phone, DbType.String);
            parameters.Add("code", organization.Code, DbType.String);
            parameters.Add("taxes", organization.Taxes, DbType.String);
            parameters.Add("address", organization.Address, DbType.String);
            parameters.Add("founder", organization.Founder, DbType.String);
            parameters.Add("account", organization.Account, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteOrganizationAsync(int id)
        {
            var query = "Delete from Organization where organization_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
        public async Task EditOrganizationAsync(int id, Organization organization)
        {
            var query = "UPDATE Organization SET name = @name, email = @email, phone = @phone, code = @code,taxes = @taxes,address = @address,founder = @founder,account = @account WHERE organization_id = @id";
            
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);
            parameters.Add("name", organization.Name, DbType.String);
            parameters.Add("email", organization.Email, DbType.String);
            parameters.Add("phone", organization.Phone, DbType.String);
            parameters.Add("code", organization.Code, DbType.String);
            parameters.Add("taxes", organization.Taxes, DbType.String);
            parameters.Add("address", organization.Address, DbType.String);
            parameters.Add("founder", organization.Founder, DbType.String);
            parameters.Add("account", organization.Account, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<int> GetCountOfOrganizationsAsync()
        {
            var query = "SELECT count(id) FROM Organization";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<int>(query);
            }
        }
        public async Task<Organization> GetOrganizationAsync(int id)
        {
            var query = "SELECT * FROM Organization Where organization_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Organization>(query, new { id });
            }
        }
        public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
        {
            var query = "SELECT * FROM Organization";
            using (var connection = _dapperContext.CreateConnection())
            {
                var organizations = await connection.QueryAsync<Organization>(query);
                return organizations.ToList();
            }
        }
    }
}
