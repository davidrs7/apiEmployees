using Api.DTO.Survey;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SurveyController : ControllerBase
    {
        private ISurveyRepository _surveyRepository;
        public SurveyController(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        [HttpGet("Surveys")]
        public async Task<ActionResult<IEnumerable<SurveyDTO>>> Surveys()
        {
            return Ok(await _surveyRepository.Surveys());
        }

        [HttpGet("{surveyId}")]
        public async Task<ActionResult<SurveyDTO>> SurveyById(int surveyId)
        {
            return Ok(await _surveyRepository.SurveyById(surveyId));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add(SurveyDTO surveyAdd)
        {
            return Ok(await _surveyRepository.Add(surveyAdd));
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromForm] SurveyDTO surveyEdit)
        {
            await _surveyRepository.Edit(surveyEdit);
            return Ok();
        }

        [HttpGet("Surveys/Header/Survey/{surveyId}")]
        public async Task<ActionResult<IEnumerable<SurveyHeaderDTO>>> HeadersBySurvey(int surveyId)
        {
            return Ok(await _surveyRepository.HeadersBySurvey(surveyId));
        }

        [HttpGet("Surveys/Header/User/{userId}")]
        public async Task<ActionResult<IEnumerable<SurveyHeaderDTO>>> HeadersByUser(int userId)
        {
            return Ok(await _surveyRepository.HeadersByUser(userId));
        }

        [HttpGet("Surveys/Header/{surveyId}/{userId}")]
        public async Task<ActionResult<SurveyHeaderDTO>> HeaderBySurveyAndUser(int surveyId, int userId)
        {
            return Ok(await _surveyRepository.HeaderBySurveyAndUser(surveyId, userId));
        }

        [HttpPost("Header/Add")]
        public async Task<ActionResult<int>> AddHeader(SurveyHeaderDTO surveyAdd)
        {
            return Ok(await _surveyRepository.AddHeader(surveyAdd));
        }

        [HttpPut("Header/Edit")]
        public async Task<ActionResult> EditHeader([FromForm] SurveyHeaderDTO surveyEdit)
        {
            await _surveyRepository.EditHeader(surveyEdit);
            return Ok();
        }

        [HttpGet("Fields")]
        public async Task<ActionResult<IEnumerable<SurveyFieldDTO>>> Fields()
        {
            return Ok(await _surveyRepository.Fields());
        }

        [HttpGet("FieldsBySurvey/{surveyId}")]
        public async Task<ActionResult<IEnumerable<SurveyFieldDTO>>> FieldsBySurvey(int surveyId)
        {
            return Ok(await _surveyRepository.FieldsBySurvey(surveyId));
        }

        [HttpGet("FieldsBySurvey/Header/{headerId}")]
        public async Task<ActionResult<IEnumerable<SurveyFieldDTO>>> FieldsByHeader(int headerId)
        {
            return Ok(await _surveyRepository.FieldsByHeader(headerId));
        }

        [HttpGet("Fields/{fieldId}")]
        public async Task<ActionResult<SurveyFieldDTO>> FieldById(int fieldId)
        {
            return Ok(await _surveyRepository.FieldById(fieldId));
        }

        [HttpPost("Fields/Add")]
        public async Task<ActionResult<int>> AddField(SurveyFieldDTO surveyAdd)
        {
            await _surveyRepository.AddField(surveyAdd);
            return Ok();
        }

        [HttpPut("Fields/Edit")]
        public async Task<ActionResult> EditField([FromForm] SurveyFieldDTO surveyEdit)
        {
            await _surveyRepository.EditField(surveyEdit);
            return Ok();
        }

        [HttpPut("Field/Rel/Merge")]
        public async Task<ActionResult> MergeSurveyFieldRel([FromForm] SurveyFieldRelDTO relMerge)
        {
            await _surveyRepository.MergeSurveyFieldRel(relMerge);
            return Ok();
        }

        [HttpGet("Users/{headerId}")]
        public async Task<ActionResult<IEnumerable<SurveyUserDTO>>> UsersByHeader(int headerId)
        {
            return Ok(await _surveyRepository.UsersByHeader(headerId));
        }

        [HttpPost("Users/Rel/Add")]
        public async Task<ActionResult<int>> AddUserRel(SurveyUserRelDTO userRel)
        {
            return Ok(await _surveyRepository.AddUserRel(userRel));
        }

        [HttpPost("Users/Rel/Add/Department")]
        public async Task<ActionResult<int>> AddUserRelByDepartment(SurveyUserRelDTO userRel)
        {
            return Ok(await _surveyRepository.AddUserRelByDepartment(userRel));
        }

        [HttpPost("Users/Rel/Add/Job")]
        public async Task<ActionResult<int>> AddUserRelByJob(SurveyUserRelDTO userRel)
        {
            return Ok(await _surveyRepository.AddUserRelByJob(userRel));
        }

        [HttpPost("Users/Rel/Add/City")]
        public async Task<ActionResult<int>> AddUserRelByCity(SurveyUserRelDTO userRel)
        {
            return Ok(await _surveyRepository.AddUserRelByCity(userRel));
        }

        [HttpPost("Users/Rel/Delete")]
        public async Task<ActionResult<int>> DeleteUserRel(SurveyUserRelDTO userRel)
        {
            return Ok(await _surveyRepository.DeleteUserRel(userRel));
        }
    }
}
