using Api.DTO.Postulate;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PostulateController : ControllerBase
    {
        private IPostulateRepository _postulateRepository;
        public PostulateController(IPostulateRepository postulateRepository)
        {
            _postulateRepository = postulateRepository;
        }

        [HttpGet("Postulates")]
        public async Task<ActionResult<IEnumerable<PostulateBasicDTO>>> AllPostulates()
        {
            return Ok(await _postulateRepository.Postulates());
        }

        [HttpPost("Postulates/Criteria")]
        public async Task<ActionResult<IEnumerable<PostulateBasicDTO>>> Postulates([FromForm] PostulateCriteriaDTO criteria)
        {
            return Ok(await _postulateRepository.Postulates(criteria));
        }

        [HttpGet("{postulateId}")]
        public async Task<ActionResult<PostulateDTO>> PostulateById(int postulateId)
        {
            return Ok(await _postulateRepository.PostulateById(postulateId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromForm] PostulateMergeDTO postulateAdd)
        {
            return Ok(await _postulateRepository.Add(postulateAdd));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] PostulateMergeDTO postulateEdit)
        {
            await _postulateRepository.Edit(postulateEdit);
            return Ok();
        }

        [HttpPut("ToEmployee")]
        public async Task<ActionResult> ToEmployee([FromForm] PostulateToEmployeeDTO toEmployee)
        {
            await _postulateRepository.ToEmployee(toEmployee);
            return Ok();
        }
    }
}
