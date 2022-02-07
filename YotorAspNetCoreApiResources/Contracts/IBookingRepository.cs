using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<Booking> GetBookingAsync(int id);
        Task CreateBookingAsync(int? restriction_id, int user_id, int car_id, int? feedback_id, DateTime start_date, DateTime end_date, bool status, int full_price, string start_address, string end_address);
        Task<Booking> GetBookingByParamsAsync(DateTime start_date, DateTime end_date, string start_address, string end_address);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int id);

    }
}
