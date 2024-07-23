using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.JobGoal;

namespace Api.Interfaces
{
    public interface IJobGoalRepository
    {
        public Task<IEnumerable<JobGoalDTO>> Goals(int jobId);
        public Task<IEnumerable<JobGoalDTO>> AddGoals(int jobId);
        public Task<IEnumerable<JobGoalDTO>> EditGoals(int jobId,int goalId);
        public Task<IEnumerable<JobGoalDTO>> DelGoals(int jobId, int goalId);
    }
}
