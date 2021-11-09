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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DapperContext _dapperContext;
        public OrganizationRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task CreateOrganization(Organization organization)
        {
            var query = "INSERT INTO Organization (name,email,phone,code,taxes,address,founder,account) values (@name,@email,@phone,@code,@taxes,@address,@founder,@account);";
            var parameters = new DynamicParameters();
            parameters.Add("name", organization.name, DbType.String);
            parameters.Add("email", organization.email, DbType.String);
            parameters.Add("phone", organization.phone, DbType.String);
            parameters.Add("code", organization.code, DbType.String);
            parameters.Add("taxes", organization.taxes, DbType.String);
            parameters.Add("address", organization.address, DbType.String);
            parameters.Add("founder", organization.founder, DbType.String);
            parameters.Add("account", organization.account, DbType.String);


            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task EditOrganization(int id, Organization organization)
        {
            var query = "UPDATE Organization SET name = @name, email = @email, phone = @phone, code = @code,taxes = @taxes,address = @address,founder = @founder,account = @account WHERE organization_id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);
            parameters.Add("name", organization.name, DbType.String);
            parameters.Add("email", organization.email, DbType.String);
            parameters.Add("phone", organization.phone, DbType.String);
            parameters.Add("code", organization.code, DbType.String);
            parameters.Add("taxes", organization.taxes, DbType.String);
            parameters.Add("address", organization.address, DbType.String);
            parameters.Add("founder", organization.founder, DbType.String);
            parameters.Add("account", organization.account, DbType.String);


            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Organization> GetOrganization(int id)
        {
            var query = "SELECT * FROM Organization Where organization_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var organization = await connection.QuerySingleOrDefaultAsync<Organization>(query, new { id });
                return organization;
            }
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
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
