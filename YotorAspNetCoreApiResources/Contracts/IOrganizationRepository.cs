using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IOrganizationRepository
    {
        public Task<IEnumerable<Organization>> GetOrganizations();
        public Task<Organization> GetOrganization(int id);
        public Task EditOrganization(int id, Organization organization);
        public Task CreateOrganization(Organization organization);
    }
}
