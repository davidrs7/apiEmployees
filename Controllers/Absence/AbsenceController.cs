using Api.DTO.Absence;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AbsenceController: ControllerBase
    {
        private IAbsenceRepository _absenceRepository;
        public AbsenceController(IAbsenceRepository absenceRepository)
        {
            _absenceRepository = absenceRepository;
        }

        [HttpGet("Users/{userId}")]
        public async Task<ActionResult<IEnumerable<AbsenceUserDTO>>> Users(int userId)
        {
            return Ok(await _absenceRepository.Users(userId));
        }

        [HttpGet("User/{employeeId}")]
        public async Task<ActionResult<AbsenceUserDTO>> UserAbsence(int employeeId)
        {
            return Ok(await _absenceRepository.User(employeeId));
        }

        [HttpGet("Absences/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AbsenceDTO>>> Absences(int employeeId)
        {
            return Ok(await _absenceRepository.Absences(employeeId));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AbsenceDTO>> Absence(int Id)
        {
            return Ok(await _absenceRepository.Absence(Id));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromForm] AbsenceDTO absenceAdd)
        {
            return Ok(await _absenceRepository.Add(absenceAdd));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] AbsenceDTO absenceEdit)
        {
            await _absenceRepository.Edit(absenceEdit);
            return Ok();
        }

        [HttpGet("Files/{absenceId}")]
        public async Task<ActionResult<IEnumerable<AbsenceFileDTO>>> Files(int absenceId)
        {
            return Ok(await _absenceRepository.Files(absenceId));
        }

        [HttpPost("Add/File")]
        public async Task<ActionResult<int>> AddFile([FromForm] AbsenceFileDTO file)
        {
            return Ok(await _absenceRepository.AddFile(file));
        }

        [HttpGet("Approvals/{absenceId}")]
        public async Task<ActionResult<IEnumerable<AbsenceApprovalDTO>>> Approvals(int absenceId)
        {
            return Ok(await _absenceRepository.Approvals(absenceId));
        }

        [HttpPost("Approval/Add")]
        public async Task<ActionResult<int>> Add([FromForm] AbsenceApprovalDTO approvalAdd)
        {
            return Ok(await _absenceRepository.AddApproval(approvalAdd));
        }

        [HttpPut("Approval/Edit")]
        public async Task<ActionResult> Edit([FromForm] AbsenceApprovalDTO approvalEdit)
        {
            await _absenceRepository.EditApproval(approvalEdit);
            return Ok();
        }
    }
}