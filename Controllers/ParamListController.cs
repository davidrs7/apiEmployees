using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ParamListController: ControllerBase
    {
        private IParamListRepository _paramListRepository;
        public ParamListController(IParamListRepository paramListRepository)
        {
            _paramListRepository = paramListRepository;
        }

        [HttpGet("DocType")]
        public async Task<ActionResult> DocTypeParams()
        {
            return Ok(await _paramListRepository.DocTypeParams());
        }

        [HttpGet("City")]
        public async Task<ActionResult> CityParams()
        {
            return Ok(await _paramListRepository.CityParams());
        }

        [HttpGet("MaritalStatus")]
        public async Task<ActionResult> MaritalStatusParams()
        {
            return Ok(await _paramListRepository.MaritalStatusParams());
        }

        [HttpGet("PrmHousingType")]
        public async Task<ActionResult> HousingTypeParams()
        {
            return Ok(await _paramListRepository.HousingTypeParams());
        }

        [HttpGet("EducationalLevel")]
        public async Task<ActionResult> EducationalLevelParams()
        {
            return Ok(await _paramListRepository.EducationalLevelParams());
        }

        [HttpGet("EmployeeStatus")]
        public async Task<ActionResult> EmployeeStatusParams()
        {
            return Ok(await _paramListRepository.EmployeeStatusParams());
        }

        [HttpGet("ContractType")]
        public async Task<ActionResult> ContractTypeParams()
        {
            return Ok(await _paramListRepository.ContractTypeParams());
        }

        [HttpGet("BankingEntity")]
        public async Task<ActionResult> BankingEntityParams()
        {
            return Ok(await _paramListRepository.BankingEntityParams());
        }

        [HttpGet("Transportation")]
        public async Task<ActionResult> TransportationParams()
        {
            return Ok(await _paramListRepository.TransportationParams());
        }

        [HttpGet("VacantStatus")]
        public async Task<ActionResult> VacantStatusParams()
        {
            return Ok(await _paramListRepository.VacantStatusParams());
        }

        [HttpGet("PostulateFindOut")]
        public async Task<ActionResult> PostulateFindOutParams()
        {
            return Ok(await _paramListRepository.PostulateFindOutParams());
        }

        [HttpGet("JobSkills")]
        public async Task<ActionResult> JobSkillsParams()
        {
            return Ok(await _paramListRepository.JobSkillsParams());
        }

        [HttpGet("Department")]
        public async Task<ActionResult> DepartmentParams()
        {
            return Ok(await _paramListRepository.DepartmentParams());
        }

        [HttpGet("Job")]
        public async Task<ActionResult> JobParams()
        {
            return Ok(await _paramListRepository.JobParams());
        }

        [HttpGet("AbsenceType")]
        public async Task<ActionResult> AbsenceTypeParams()
        {
            return Ok(await _paramListRepository.AbsenceTypeParams());
        }
    }
}
