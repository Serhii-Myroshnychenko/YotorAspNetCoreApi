using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface ILandlordRepository
    {
        public Task<IEnumerable<Landlord>> GetLandlords();
        public Task<Landlord> GetLandlord(int id);
        public Task UpdateLandlord(int id, Landlord landlord);
        public Task CreateLandlord(Landlord landlord);


    }
}
