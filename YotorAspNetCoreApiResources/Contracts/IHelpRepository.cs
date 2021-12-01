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
        public Task<Landlord> IsLandlord(int id);
        public Task<bool> IsAdmin(int id);
        public Task<bool> IsUser(int id);
        public Task<bool> IsOrganization(int id);
        public Task<Landlord> IsThisCarOfHisOrganization(string name);
        public Task<Restriction> GetRestrictionByCarName(string name);
        public Task<Car> GetCarByCarName(string name);
        public Task UpdateStatusCar(int id);
        public Task<Customer> GetCustomerByName(string name);
        public Task<Organization> GetOrganizationByName(string name);

    }
}
