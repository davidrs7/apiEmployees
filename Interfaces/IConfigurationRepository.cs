using Api.DTO.Configuration;

namespace Api.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<IEnumerable<ConfigurationItemDTO>> ConfigurationItems(string configKey);
        Task MergeConfiguration(ConfigurationDTO configDTO);
        Task SetConfigurationItem(ConfigurationItemDTO itemDTO);
        Task DeleteConfigurationItem(ConfigurationItemDTO itemDTO);
        Task<IEnumerable<ConfigurationUserDTO>> UsersByAbsenceTypesAllowed();
    }
}