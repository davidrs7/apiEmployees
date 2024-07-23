namespace Api.DTO.Configuration
{
    public class ConfigurationDTO
    {
        public string ConfigKey { get; set; } = String.Empty;
        public int UserId { get; set; }
        public DateTime Updated { get; set; }
        public string Value1 { get; set; } = String.Empty;
        public string Value2 { get; set; } = String.Empty;
        public string ListType { get; set; } = String.Empty;
    }
}