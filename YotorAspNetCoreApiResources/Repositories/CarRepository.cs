﻿using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Context;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DapperContext _dapperContext;
        public CarRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task CreateCar(int organization_id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, byte[] photo, string description, string number)
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
            parameters.Add("photo", photo, DbType.Binary);
            parameters.Add("description", description, DbType.String);
            parameters.Add("number", number, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Car> GetCar(int id)
        {
            var query = "SELECT * FROM Car WHERE car_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var car = await connection.QuerySingleOrDefaultAsync<Car>(query, new { id });
                return car;

            }
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            var query = "Select * from Car";
            using (var connection = _dapperContext.CreateConnection())
            {
                var cars = await connection.QueryAsync<Car>(query);
                return cars.ToList();
            }
        }
        public async Task<Landlord> IsLandlord(int id)
        {
            var query = "Select * from Landlord Where user_id = @id";
            using(var connection = _dapperContext.CreateConnection())
            {
                var landlord = await connection.QuerySingleOrDefaultAsync<Landlord>(query, new { id});
                return landlord;
            }
        }
    }
}
