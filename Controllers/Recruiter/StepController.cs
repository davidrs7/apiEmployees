using Api.DTO.Step;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class StepController : ControllerBase
    {
        private IStepRepository _stepRepository;
        public StepController(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }

        [HttpGet("Steps")]
        public async Task<ActionResult<IEnumerable<StepDTO>>> Steps()
        {
            return Ok(await _stepRepository.Steps());
        }

        [HttpGet("{stepId}")]
        public async Task<ActionResult<StepDTO>> StepById(int stepId)
        {
            return Ok(await _stepRepository.StepById(stepId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add(StepDTO stepAdd)
        {
            return Ok(await _stepRepository.Add(stepAdd));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] StepDTO stepEdit)
        {
            await _stepRepository.Edit(stepEdit);
            return Ok();
        }

        [HttpGet("Fields")]
        public async Task<ActionResult<IEnumerable<StepFieldDTO>>> Fields()
        {
            return Ok(await _stepRepository.Fields());
        }

        [HttpGet("Fields/{fieldId}")]
        public async Task<ActionResult<StepFieldDTO>> FieldById(int fieldId)
        {
            return Ok(await _stepRepository.FieldById(fieldId));
        }

        [HttpGet("FieldsByStep/{stepId}")]
        public async Task<ActionResult<IEnumerable<StepFieldRelDTO>>> FieldsByStep(int stepId)
        {
            return Ok(await _stepRepository.FieldsByStep(stepId));
        }

        [HttpGet("FieldsByStepRel/{stepId}/{vacantId}/{postulateId}")]
        public async Task<ActionResult<IEnumerable<StepFieldRelValueDTO>>> FieldsByStepRel(int stepId, int vacantId, int postulateId)
        {
            return Ok(await _stepRepository.FieldsByStepRel(stepId, vacantId, postulateId));
        }

        [HttpPost("Field/Add")]
        public async Task<ActionResult> AddField(StepFieldDTO stepAdd)
        {
            await _stepRepository.AddField(stepAdd);
            return Ok();
        }

        [HttpPut("Field/Edit")]
        public async Task<ActionResult> EditField([FromForm] StepFieldDTO stepEdit)
        {
            await _stepRepository.EditField(stepEdit);
            return Ok();
        }

        [HttpPut("Field/Rel/Merge")]
        public async Task<ActionResult> MergeStepFieldRel([FromForm] StepFieldRelDTO relMerge)
        {
            await _stepRepository.MergeStepFieldRel(relMerge);
            return Ok();
        }

        [HttpPut("Field/Postulate/Rel/Merge")]
        public async Task<ActionResult> MergeStepFieldRelValue([FromForm] StepFieldRelValueDTO relMerge)
        {
            await _stepRepository.MergeStepFieldRelValue(relMerge);
            return Ok();
        }
    }
}
