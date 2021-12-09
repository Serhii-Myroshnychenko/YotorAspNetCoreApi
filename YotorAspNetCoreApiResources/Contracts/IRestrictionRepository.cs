using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IRestrictionRepository
    {
        public Task<IEnumerable<Restriction>> GetRestrictionsAsync();
        public Task<Restriction> GetRestrictionAsync(int id);
        public Task CreateRestrictionAsync( int landlord_id, string car_name, string description);
        public Task UpdateRestrictionAsync(int id, Restriction restriction);
        public Task DeleteRestrictionAsync(int id);
    }
}
