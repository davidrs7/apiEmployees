namespace Api.DTO.Postulate
{
    public class PostulateDTO
    {
        public int Id { get; set; }
        public int RecruiterUserId { get; set; }
        public int FindOutId { get; set; }
        public string FindOutName { get; set; } = String.Empty;
        public int DocTypeId { get; set; }
        public string DocTypeName { get; set; } = String.Empty;
        public int EducationalLevelId { get; set; }
        public string EducationalLevelName { get; set; } = String.Empty;
        public double OfferedSalary { get; set; }
        public double ExpectedSalary { get; set; }
        public string Doc { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set;} = String.Empty;
        public string Sex { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
        public string Rh { get; set; } = String.Empty;
        public string CellPhone { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Career { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string PhotoUrl { get; set; } = String.Empty;
        public string ColorHex { get; set; } = String.Empty;
    }
}
