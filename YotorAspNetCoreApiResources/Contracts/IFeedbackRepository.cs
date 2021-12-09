using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IFeedbackRepository
    {
        public Task<IEnumerable<Feedback>> GetFeedbacksAsync();
        public Task<Feedback> GetFeedbackAsync(int id);
        public Task CreateFeedbackAsync(int user_id, string name, DateTime date, string text);
        public Task DeleteFeedbackAsync(int id);
    }
}
