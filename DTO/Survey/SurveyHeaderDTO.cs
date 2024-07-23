namespace Api.DTO.Survey
{
    public class SurveyHeaderDTO
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public int Avalable { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string Title { get; set; } = String.Empty;
        public int? IsAnswered { get; set; }
        public int? Draft { get; set; }
    }
}
