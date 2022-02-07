using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IRestrictionRepository
    {
        Task<IEnumerable<Restriction>> GetRestrictionsAsync();
        Task<Restriction> GetRestrictionAsync(int id);
        Task CreateRestrictionAsync( int landlord_id, string car_name, string description);
        Task UpdateRestrictionAsync(int id, Restriction restriction);
        Task DeleteRestrictionAsync(int id);
    }
}
