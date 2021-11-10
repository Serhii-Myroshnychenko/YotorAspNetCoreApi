using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IFeedbackRepository
    {
        public Task<IEnumerable<Feedback>> GetFeedbacks();
        public Task<Feedback> GetFeedback(int id);
        public Task CreateFeedback(int user_id, string name, DateTime date, string text);
        public Task DeleteFeedback(int id);
    }
}
