using Api.DTO.Job;

namespace Api.Interfaces
{
    public interface IJobRepository
    {
        Task<IEnumerable<JobBasicDTO>> Jobs();
        Task<JobDTO> JobById(int jobId);
        Task<int> Add(JobDTO jobAdd);
        Task Edit(JobDTO jobEdit);
    }
}