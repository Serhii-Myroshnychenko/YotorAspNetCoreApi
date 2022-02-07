using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorContext.Context;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DapperContext _dappperContext;
        public BookingRepository(DapperContext dapperContext)
        {
            _dappperContext = dapperContext;
        }
        public async Task CreateBookingAsync(int? restriction_id, int user_id, int car_id, int? feedback_id, DateTime start_date, DateTime end_date, bool status, int full_price, string start_address, string end_address)
        {
            var query = "INSERT INTO Booking (restriction_id,user_id,car_id,feedback_id,start_date,end_date,status,full_price, start_address, end_address) values (@restriction_id,@user_id,@car_id,@feedback_id,@start_date,@end_date,@status,@full_price,@start_address, @end_address);";
            
            var parameters = new DynamicParameters();
            parameters.Add("restriction_id", restriction_id, DbType.Int64);
            parameters.Add("user_id", user_id, DbType.Int64);
            parameters.Add("car_id", car_id, DbType.Int64);
            parameters.Add("feedback_id", feedback_id, DbType.Int64);
            parameters.Add("start_date", start_date, DbType.DateTime);
            parameters.Add("end_date", end_date, DbType.Date);
            parameters.Add("status", status, DbType.Boolean);
            parameters.Add("full_price", full_price, DbType.Int64);
            parameters.Add("start_address", start_address, DbType.String);
            parameters.Add("end_address", end_address, DbType.String);

            using (var connection = _dappperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<Booking> GetBookingAsync(int id)
        {
            var query = "SELECT * FROM Booking WHERE booking_id = @id";
            using (var connection = _dappperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Booking>(query, new { id });
            }
        }
        public async Task<Booking> GetBookingByParamsAsync(DateTime start_date, DateTime end_date, string start_address, string end_address)
        {
            var query = "SELECT * FROM Booking WHERE  start_date = @start_date and end_date = @end_date and start_address = @start_address and end_address= @end_address";
            
            var parameters = new DynamicParameters();
            parameters.Add("start_date", start_date, DbType.DateTime);
            parameters.Add("end_date", end_date, DbType.DateTime);
            parameters.Add("start_address", start_address, DbType.String);
            parameters.Add("end_address", end_address, DbType.String);

            using(var connection = _dappperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Booking>(query,parameters);
            }
        }
        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int id)
        {
            var query = "select * from Booking where user_id = @id";
            using(var connection = _dappperContext.CreateConnection())
            {
                var bookings = await connection.QueryAsync<Booking>(query, new { id});
                return bookings.ToList();
            }
        }
        public async Task<IEnumerable<Booking>> GetBookingsAsync()
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
