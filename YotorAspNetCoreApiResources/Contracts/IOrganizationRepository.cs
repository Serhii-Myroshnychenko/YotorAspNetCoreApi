using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IOrganizationRepository
    {
        public Task<IEnumerable<Organization>> GetOrganizationsAsync();
        public Task<Organization> GetOrganizationAsync(int id);
        public Task EditOrganizationAsync(int id, Organization organization);
        public Task CreateOrganizationAsync(Organization organization);
    }
}
