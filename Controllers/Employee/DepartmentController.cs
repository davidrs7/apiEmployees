using Api.DTO.Department;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DepartmentController: ControllerBase
    {
        private IDepartmentRepository _departmentsRepository;
        public DepartmentController(IDepartmentRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        [HttpGet("Departments")]
        public async Task<ActionResult> Departments()
        {
            return Ok(await _departmentsRepository.Departments());
        }

        [HttpGet("Id/{departmentId}")]
        public async Task<ActionResult<DepartmentDTO>> DepartmentById(int departmentId)
        {
            return Ok(await _departmentsRepository.DepartmentById(departmentId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(DepartmentDTO departmentAdd)
        {
            await _departmentsRepository.Add(departmentAdd);
            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] DepartmentDTO departmentEdit)
        {
            await _departmentsRepository.Edit(departmentEdit);
            return Ok();
        }
    }
}