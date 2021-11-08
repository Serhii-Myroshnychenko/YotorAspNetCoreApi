using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface ICarRepository
    {
        public Task<IEnumerable<Car>> GetCars();
        public Task<Car> GetCar(int id);
        public Task CreateCar(int organization_id,string model, string brand, string year, string transmission, string address, bool status, string type, int price, byte[] phote, string description, string number );

        public Task<Landlord> IsLandlord(int id);
        public Task<bool> IsAdmin(int id);
        public Task UpdateCar(int id,string model, string brand, string year, string transmission, string address, bool status, string type, int price, byte[] phote, string description, string number);
    }
}
