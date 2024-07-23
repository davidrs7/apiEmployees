using Api.DTO.Job;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class JobController: ControllerBase
    {
        private IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet("Jobs")]
        public async Task<ActionResult<IEnumerable<JobBasicDTO>>> Jobs()
        {
            return Ok(await _jobRepository.Jobs());
        }

        [HttpGet("Id/{jobId}")]
        public async Task<ActionResult<JobDTO>> JobById(int jobId)
        {
            return Ok(await _jobRepository.JobById(jobId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromForm] JobDTO jobAdd) 
        {
            return Ok(await _jobRepository.Add(jobAdd));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] JobDTO jobEdit)
        {
            await _jobRepository.Edit(jobEdit);
            return Ok();
        }
    }
}