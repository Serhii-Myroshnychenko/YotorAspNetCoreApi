using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Context;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;
using System.Data;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DapperContext _dapperContext;
        public FeedbackRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task CreateFeedback(int user_id, string name, DateTime date, string text)
        {
            var query = "INSERT INTO Feedback (user_id, name, date,text) values (@user_id, @name, @date,@text);";
            var parameters = new DynamicParameters();
            parameters.Add("user_id", user_id, DbType.Int64);
            parameters.Add("name", name, DbType.String);
            parameters.Add("date", date, DbType.DateTime);
            parameters.Add("text", text, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteFeedback(int id)
        {
            var query = "Delete from Feedback where feedback_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.QuerySingleOrDefaultAsync<Feedback>(query, new { id });
            }
        }

        public async Task<Feedback> GetFeedback(int id)
        {
            var query = "Select * from Feedback where feedback_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var feedback = await connection.QuerySingleOrDefaultAsync<Feedback>(query, new { id });
                return feedback;
            }
        }

        public async  Task<IEnumerable<Feedback>> GetFeedbacks()
        {
            var query = "Select * from Feedback";
            using (var connection = _dapperContext.CreateConnection())
            {
                var feedbacks = await connection.QueryAsync<Feedback>(query);
                return feedbacks.ToList();
            }
        }
    }
}
