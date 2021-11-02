using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Context;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DapperContext _dappperContext;
        public BookingRepository(DapperContext dapperContext)
        {
            _dappperContext = dapperContext;
        }

        public Task CreateBooking()
        {
            throw new NotImplementedException();
        }

        public Task EditBooking()
        {
            throw new NotImplementedException();
        }

        public async Task<Booking> GetBooking(int id)
        {
            var query = "SELECT * FROM Booking WHERE booking_id = @id";
            using (var connection = _dappperContext.CreateConnection())
            {
                var cust = await connection.QuerySingleOrDefaultAsync<Booking>(query, new { id });
                return cust;

            }
        }

        public async Task<IEnumerable<Booking>> GetBookings()
        {
            var query = "SELECT * FROM Booking";
            using (var connection = _dappperContext.CreateConnection())
            {
                var customers = await connection.QueryAsync<Booking>(query);
                return customers.ToList();
            }
        }
    }
}
