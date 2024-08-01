using Api.DTO.Employee;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmployeeController: ControllerBase
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost("Employees/All")]
        public async Task<ActionResult<IEnumerable<EmployeeBasicDTO>>> EmployeesAll([FromForm] EmployeeCriteriaDTO employeeCriteria)
        {
            return Ok(await _employeeRepository.EmployeesAll(employeeCriteria));
        }

        [HttpPost("Employees/Criteria")]
        public async Task<ActionResult<IEnumerable<EmployeeBasicDTO>>> Employees([FromForm] EmployeeCriteriaDTO employeeCriteria)
        {
            return Ok(await _employeeRepository.Employees(employeeCriteria));
        }

        [HttpPost("Employees/Download")]
        public async Task<ActionResult<IEnumerable<EmployeeBasicDTO>>> EmployeesDownload([FromForm] EmployeeCriteriaDTO employeeCriteria)
        {
            return Ok(await _employeeRepository.EmployeesDownload(employeeCriteria));
        }

        [HttpGet("EmployeesWithoutPages/{excludeEmployeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeBasicDTO>>> EmployeesWithoutPages(int excludeEmployeeId)
        {
            return Ok(await _employeeRepository.EmployeesWithoutPages(excludeEmployeeId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromForm] EmployeeMergeDTO employeeAdd)
        {
            return Ok(await _employeeRepository.Add(employeeAdd));
        }

        [HttpPost("Add/General")]
        public async Task<ActionResult<int>> AddGeneral([FromForm] EmployeeGeneralDTO employeeAdd)
        {
            return Ok(await _employeeRepository.AddGeneral(employeeAdd));
        }

        [HttpPost("Add/Academic")]
        public async Task<ActionResult<int>> AddAcademic([FromForm] EmployeeAcademicDTO employeeAdd)
        {
            return Ok(await _employeeRepository.AddAcademic(employeeAdd));
        }

        [HttpPost("Add/File")]
        public async Task<ActionResult<int>> AddFile([FromForm] EmployeeFileMergeDTO employeeAdd)
        {
            return Ok(await _employeeRepository.AddFile(employeeAdd));
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDTO>> Employee(int employeeId)
        {
            return Ok(await _employeeRepository.Employee(employeeId));
        }

        [HttpGet("EmpGeneral/{employeeId}")]
        public async Task<ActionResult<EmployeeGeneralDTO>> EmployeeGeneral(int employeeId)
        {
            return Ok(await _employeeRepository.EmployeeGeneral(employeeId));
        }

        [HttpGet("Academic/{employeeId}")]
        public async Task<ActionResult<EmployeeAcademicDTO>> EmployeeAcademic(int employeeId)
        {
            return Ok(await _employeeRepository.EmployeeAcademic(employeeId));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] EmployeeMergeDTO employeeEdit)
        {
            await _employeeRepository.Edit(employeeEdit);
            return Ok();
        }

        [HttpPut("Edit/General")]
        public async Task<ActionResult> EditGeneral([FromForm] EmployeeGeneralDTO employeeEdit)
        {
            await _employeeRepository.EditGeneral(employeeEdit);
            return Ok();
        }

        [HttpPut("Edit/Academic")]
        public async Task<ActionResult> EditAcademic([FromForm] EmployeeAcademicDTO employeeEdit)
        {
            await _employeeRepository.EditAcademic(employeeEdit);
            return Ok();
        }

        [HttpPost("Add/EmpKnowledge")]
        public async Task<ActionResult> MergeKnowledgesAndSkills([FromForm] EmployeeKnowledgeDTO merge)
        {
            await _employeeRepository.MergeKnowledge(merge);
            return Ok();
        }

        [HttpPost("Add/EmpSkill")]
        public async Task<ActionResult> MergeSkill([FromForm] EmployeeSkillDTO merge)
        {
            await _employeeRepository.MergeSkill(merge);
            return Ok();
        }

        [HttpGet("Files/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeFileDTO>>> Files(int employeeId)
        {
            return Ok(await _employeeRepository.Files(employeeId));
        }

        [HttpGet("Skills/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeSkillDTO>>> Skills(int employeeId)
        {
            return Ok(await _employeeRepository.Skills(employeeId));
        }

        [HttpGet("Knowledges/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeKnowledgeDTO>>> Knowledges(int employeeId)
        {
            return Ok(await _employeeRepository.Knowledges(employeeId));
        }

        [HttpGet("Skills")]
        public async Task<ActionResult<IEnumerable<EmployeeSkillDTO>>> Skills()
        {
            return Ok(await _employeeRepository.Skills());
        }

        [HttpGet("Knowledges")]
        public async Task<ActionResult<IEnumerable<EmployeeKnowledgeDTO>>> Knowledges()
        {
            return Ok(await _employeeRepository.Knowledges());
        }

        [HttpGet("FileTypes")]
        public async Task<ActionResult<IEnumerable<EmployeeFileTypeDTO>>> EmployeeFileTypes()
        {
            return Ok(await _employeeRepository.EmployeeFileTypes());
        }
        [HttpGet("Sons/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeSonsDTO>>> Sons(int employeeId)
        {
            Console.WriteLine("Sons.....");
            return Ok(await _employeeRepository.Sons(employeeId));
        }
        [HttpPost("Add/EmpSon")]
        public async Task<ActionResult> SaveSon([FromForm] EmployeeSonsDTO sonData)
        {
            /* await _employeeRepository.MergeSkill();*/
            Console.WriteLine("--------SaveSons------");
            int result = 0;
            if (sonData.EmployeeGeneralId>0 && sonData.Id>0)
            {
                Console.WriteLine("----UpdateSons----");
                result = await _employeeRepository.UpdateSons(sonData);
            }
            else
            {
                Console.WriteLine("----AddSons----");
                result =await _employeeRepository.AddSons(sonData);
            }
            return Ok(result);
        }
        [HttpPost("Del/EmpSon")]
        public async Task<ActionResult> DelSon([FromForm] int id)
        {
            int result = 0;
            Console.WriteLine("--->" + id);
            result = await _employeeRepository.DelSons(id);
            return Ok(result);
        }

    }
}