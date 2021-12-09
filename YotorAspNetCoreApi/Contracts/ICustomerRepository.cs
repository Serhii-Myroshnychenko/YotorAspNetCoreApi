using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApi.Models;

namespace YotorAspNetCoreApi.Contracts
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetCustomersAsync();
        public Customer GetCustomerAsync(string email, string password);
        public Task Registration(string full_name, string email, string phone, string password, bool is_admin);
    }
}
