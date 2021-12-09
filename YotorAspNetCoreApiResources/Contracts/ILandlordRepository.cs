using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface ILandlordRepository
    {
        public Task<IEnumerable<Landlord>> GetLandlordsAsync();
        public Task<Landlord> GetLandlordAsync(int id);
        public Task UpdateLandlordAsync(int id, Landlord landlord);
        public Task CreateLandlordAsync(int user_id, int organization_id, string name);


    }
}
