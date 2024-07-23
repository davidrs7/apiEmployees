using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.Job;

namespace Api.Interfaces
{
    public interface IJobGoalHeader
    {
        public Task<IEnumerable<JobGoalHeaderDTO>> GoalHeaders(int goalId);
        public Task<IEnumerable<JobGoalHeaderDTO>> AddGoalHeaders();
        public Task<IEnumerable<JobGoalHeaderDTO>> EditGoalHeaders(int goalId, int headerId);
        public Task<IEnumerable<int>> DelGoalHeaders(int goalId, int headerId);
    }
}
