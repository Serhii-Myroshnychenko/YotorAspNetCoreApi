using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetBookings();
        public Task<Booking> GetBooking(int id);
        public Task EditBooking();
        public Task CreateBooking();
    }
}
