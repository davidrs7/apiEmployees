namespace Api.DTO.Survey
{
    public class SurveyDTO
    {
        public int Id { get; set; }
        public int Available { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}
