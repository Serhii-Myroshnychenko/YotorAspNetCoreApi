using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApi.Context;
using YotorAspNetCoreApi.Contracts;
using YotorAspNetCoreApi.Models;

namespace YotorAspNetCoreApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _dapperContext;
        public CustomerRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public Customer GetCustomerAsync(string email, string password)
        {
            var query = "Select * from Customer where email = @email";
            
            using (var connection = _dapperContext.CreateConnection())
            {
                var customer = connection.QuerySingleOrDefault<Customer>(query, new { email });
                bool isValid = BCrypt.Net.BCrypt.Verify(password, customer.Password);
                if (isValid)
                {
                    return customer;
                }
                return null;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var query = "Select * From Customer";
            using (var connection = _dapperContext.CreateConnection())
            {
                var customers = await connection.QueryAsync<Customer>(query);
                return customers.ToList();
            }
        }

        public async Task Registration(string full_name, string email, string phone, string password, bool is_admin)
        {
            var query = "Insert into Customer (full_name,email,phone,password,is_admin) VALUES (@full_name,@email,@phone,@password,@is_admin)";

            var parameters = new DynamicParameters();
            parameters.Add("full_name", full_name, DbType.String);
            parameters.Add("email", email, DbType.String);
            parameters.Add("phone", phone, DbType.String);
            parameters.Add("password", BCrypt.Net.BCrypt.HashPassword(password), DbType.String);
            parameters.Add("is_admin", is_admin, DbType.Boolean);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
