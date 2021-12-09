using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApi.Models;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IHelpRepository
    {
        public Task<Landlord> IsLandlordAsync(int id);
        public Task<bool> IsAdminAsync(int id);
        public Task<bool> IsUserAsync(int id);
        public Task<bool> IsOrganizationAsync(int id);
        public Task<Landlord> IsThisCarOfHisOrganizationAsync(string name);
        public Task<Restriction> GetRestrictionByCarNameAsync(string name);
        public Task<Car> GetCarByCarNameAsync(string name);
        public Task UpdateStatusCarAsync(int id);
        public Task<Customer> GetCustomerByNameAsync(string name);
        public Task<Organization> GetOrganizationByNameAsync(string name);
        

    }
}
