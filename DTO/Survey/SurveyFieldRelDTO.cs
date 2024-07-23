namespace Api.DTO.Survey
{
    public class SurveyFieldRelDTO
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int Available { get; set; }
        public string Name { get; set; } = String.Empty;
        public string FieldType { get; set; } = String.Empty;
        public int IsRequired { get; set; }
        public string Config { get; set; } = String.Empty;
        public int Active { get; set; }
        public int Weight { get; set; }
        public Boolean Inserted { get; set; }
        public Boolean Updated { get; set; }
    }
}