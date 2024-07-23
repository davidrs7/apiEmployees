using Api.DTO.Job;

namespace Api.Interfaces
{
    public interface IJobSkilsRepository
    {
        public Task<IEnumerable<JobSkillsDTO>> Skills(int jobId);
        public Task<IEnumerable<JobSkillsDTO>> AddSkills(int jobId);
        public Task<IEnumerable<JobSkillsDTO>> EditSkill(int jobId, int skillId);
        public Task<IEnumerable<int>> DelSkills(int jobId, int skillId);
    }
}
