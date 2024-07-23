using Api.DTO.Configuration;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConfigurationController: ControllerBase
    {
        private IConfigurationRepository _configurationRepository;
        public ConfigurationController(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        [HttpGet("Items/{configKey}")]
        public async Task<ActionResult> ConfigurationItems(string configKey)
        {
            return Ok(await _configurationRepository.ConfigurationItems(configKey));
        }

        [HttpPut("Merge")]
        public async Task<ActionResult> MergeConfiguration([FromForm] ConfigurationDTO configDTO)
        {
            await _configurationRepository.MergeConfiguration(configDTO);
            return Ok();
        }

        [HttpPost("Item/Add")]
        public async Task<ActionResult> SetConfigurationItem([FromForm] ConfigurationItemDTO itemDTO)
        {
            await _configurationRepository.SetConfigurationItem(itemDTO);
            return Ok();
        }

        [HttpPut("Item/Delete")]
        public async Task<ActionResult> DeleteConfigurationItem([FromForm] ConfigurationItemDTO itemDTO)
        {
            await _configurationRepository.DeleteConfigurationItem(itemDTO);
            return Ok();
        }

        [HttpGet("Users/AbsenceTypesAllowed")]
        public async Task<ActionResult> UsersByAbsenceTypesAllowed()
        {
            return Ok(await _configurationRepository.UsersByAbsenceTypesAllowed());
        }
    }
}
