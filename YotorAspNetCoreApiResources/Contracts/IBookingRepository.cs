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
        public Task CreateBooking(int? restriction_id, int user_id, int car_id, int? feedback_id, DateTime start_date, DateTime end_date, bool status, int full_price, string start_address, string end_address);
    }
}
