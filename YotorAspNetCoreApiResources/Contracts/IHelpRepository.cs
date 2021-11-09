using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IHelpRepository
    {
        public Task<Landlord> IsLandlord(int id);
        public Task<bool> IsAdmin(int id);
    }
}
