namespace Api.DTO.Survey
{
    public class SurveyUserDTO
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int HeaderId { get; set; }
        public int ResponseId { get; set; }
        public DateTime CreateAt { get; set; }
        public Boolean Available { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int EmployeeId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string PhotoUrl { get; set; } = String.Empty;
        public string JobName { get; set; } = String.Empty;
        public string DepartmentName { get; set; } = String.Empty;
    }
}