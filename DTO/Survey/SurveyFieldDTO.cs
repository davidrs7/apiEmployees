namespace Api.DTO.Survey
{
    public class SurveyFieldDTO
    {
        public int Id { get; set; }
        public int Available { get; set; }
        public string Name { get; set; } = String.Empty;
        public string FieldType { get; set; } = String.Empty;
        public int IsRequired { get; set; }
        public string? Config { get; set; } = String.Empty;
    }
}
