using Api.DTO.Vacant;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VacantController : ControllerBase
    {
        private IVacantRepository _vacantRepository;
        public VacantController(IVacantRepository vacantRepository)
        {
            _vacantRepository = vacantRepository;
        }

        [HttpGet("Vacants")]
        public async Task<ActionResult<IEnumerable<VacantDTO>>> Vacants()
        {
            return Ok(await _vacantRepository.Vacants());
        }

        [HttpGet("{vacantId}")]
        public async Task<ActionResult<VacantDTO>> VacantById(int vacantId)
        {
            return Ok(await _vacantRepository.VacantById(vacantId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add(VacantDTO vacantAdd)
        {
            return Ok(await _vacantRepository.Add(vacantAdd));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] VacantDTO vacantEdit)
        {
            await _vacantRepository.Edit(vacantEdit);
            return Ok();
        }

        [HttpGet("Steps/{vacantId}")]
        public async Task<ActionResult<IEnumerable<VacantStepDTO>>> VacantSteps(int vacantId)
        {
            return Ok(await _vacantRepository.VacantSteps(vacantId));
        }

        [HttpGet("StepsByPostRel/{vacantId}/{postulateId}")]
        public async Task<ActionResult<IEnumerable<VacantStepPostulateRelDTO>>> VacantStepsByPostulateRel(int vacantId, int postulateId)
        {
            return Ok(await _vacantRepository.VacantStepsByPostulateRel(vacantId, postulateId));
        }

        [HttpGet("Postulate/{postulateId}")]
        public async Task<ActionResult<IEnumerable<VacantPostulateRelDTO>>> VacantsByPostulate(int postulateId)
        {
            return Ok(await _vacantRepository.VacantsByPostulate(postulateId));
        }

        [HttpGet("PostulateByVacant/{vacantId}")]
        public async Task<ActionResult<IEnumerable<VacantPostulateRelDTO>>> VacantsByVacantRel(int vacantId)
        {
            return Ok(await _vacantRepository.VacantsByVacantRel(vacantId));
        }

        [HttpPut("Step/Rel/Merge")]
        public async Task<ActionResult> MergeVacantStepRel([FromForm] VacantStepDTO relMerge)
        {
            await _vacantRepository.MergeVacantStepRel(relMerge);
            return Ok();
        }

        [HttpPut("Step/Postulate/Rel/Merge")]
        public async Task<ActionResult> MergeVacantStepPostulateRel([FromForm] VacantStepPostulateRelDTO relMerge)
        {
            await _vacantRepository.MergeVacantStepPostulateRel(relMerge);
            return Ok();
        }

        [HttpPost("Postulate/Add")]
        public async Task<ActionResult> AddVacantPostulateRel(VacantPostulateRelDTO relMerge)
        {
            return Ok(await _vacantRepository.AddVacantPostulateRel(relMerge));
        }

        [HttpGet("Historical/Vacants")]
        public async Task<ActionResult<IEnumerable<VacantDTO>>> HistoricalVacants()
        {
            return Ok(await _vacantRepository.HistoricalVacants());
        }

        [HttpGet("Historical/{vacantId}")]
        public async Task<ActionResult<VacantDTO>> HistoricalVacantById(int vacantId)
        {
            return Ok(await _vacantRepository.HistoricalVacantById(vacantId));
        }
    }
}
