using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Context;
using YotorAspNetCoreApiResources.Contracts;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DapperContext _dapperContext;
        public FeedbackRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public Task CreateFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFeedback(int id)
        {
            throw new NotImplementedException();
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
            var query = "Select * from Feedback where feedback_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var feedbacks = await connection.QueryAsync<Feedback>(query);
                return feedbacks.ToList();
            }
        }
    }
}
