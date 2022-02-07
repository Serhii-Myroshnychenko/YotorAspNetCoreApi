using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorContext.Context;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DapperContext _dapperContext;
        public CarRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task CreateCarAsync(int organization_id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, string photo, string description, string number)
        {
            var query = "INSERT INTO Car (organization_id,model,brand,year,transmission,address,status,type,price,photo,description,number) VALUES (@organization_id,@model,@brand,@year,@transmission,@address,@status,@type,@price,@photo,@description,@number)";
            var parameters = new DynamicParameters();
            parameters.Add("organization_id", organization_id, DbType.Int32);
            parameters.Add("model", model, DbType.String);
            parameters.Add("brand", brand, DbType.String);
            parameters.Add("year", year, DbType.String);
            parameters.Add("transmission", transmission, DbType.String);
            parameters.Add("address", address, DbType.String);
            parameters.Add("status", status, DbType.Boolean);
            parameters.Add("type", type, DbType.String);
            parameters.Add("price", price, DbType.Int64);
            parameters.Add("photo", photo, DbType.String);
            parameters.Add("description", description, DbType.String);
            parameters.Add("number", number, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<Car> GetCarAsync(int id)
        {
            var query = "SELECT * FROM Car WHERE car_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Car>(query, new { id });
            }
        }
        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            var query = "Select * from Car";
            using (var connection = _dapperContext.CreateConnection())
            {
                var cars = await connection.QueryAsync<Car>(query);
                return cars.ToList();
            }
        }
        public async Task UpdateCarAsync(int id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, string photo, string description, string number)
        {
            var query = "UPDATE Car SET model = @model, brand = @brand, year = @year, transmission = @transmission, address = @address,status = @status,type = @type,price = @price,photo = @photo,description = @description,number = @number WHERE car_id = @id";
            
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("model", model, DbType.String);
            parameters.Add("brand", brand, DbType.String);
            parameters.Add("year", year, DbType.String);
            parameters.Add("transmission", transmission, DbType.String);
            parameters.Add("address", address, DbType.String);
            parameters.Add("status", status, DbType.Boolean);
            parameters.Add("type", type, DbType.String);
            parameters.Add("price", price, DbType.Int64);
            parameters.Add("photo", photo, DbType.String);
            parameters.Add("description", description, DbType.String);
            parameters.Add("number", number, DbType.String);
            
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<IEnumerable<Car>> GetMostPopularCarsAsync()
        {
            var query = "select Car.car_id, Car.organization_id, Car.model, Car.brand, Car.year, Car.transmission, Car.address, Car.status, Car.type, Car.price, Car.photo, Car.description, Car.number from Car left join Booking on Car.car_id = Booking.car_id group by Car.car_id, Car.organization_id, Car.model, Car.brand, Car.year, Car.transmission, Car.address, Car.status, Car.type, Car.price, Car.photo, Car.description, Car.number having count(Booking.booking_id) >= 0 order by COUNT(Booking.booking_id) desc";
            using (var connection = _dapperContext.CreateConnection())
            {
                var cars = await connection.QueryAsync<Car>(query);
                return cars.ToList();
            }
        }
    }
}
