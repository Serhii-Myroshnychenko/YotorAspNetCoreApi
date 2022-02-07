using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface ILandlordRepository
    {
        Task<IEnumerable<Landlord>> GetLandlordsAsync();
        Task<Landlord> GetLandlordAsync(int id);
        Task UpdateLandlordAsync(int id, Landlord landlord);
        Task CreateLandlordAsync(int user_id, int organization_id, string name);


    }
}
