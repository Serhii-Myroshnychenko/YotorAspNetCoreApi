using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IRestrictionRepository
    {
        public Task<IEnumerable<Restriction>> GetRestrictions();
        public Task<Restriction> GetRestriction(int id);
        public Task CreateRestriction(Restriction restriction);
        public Task UpdateRestriction(int id, Restriction restriction);
        public Task DeleteRestriction(int id);
    }
}
