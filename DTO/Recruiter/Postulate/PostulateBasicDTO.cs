namespace Api.DTO.Postulate
{
    public class PostulateBasicDTO
    {
        public int Id { get; set; }
        public string EducationalLevelName { get; set; } = String.Empty;
        public double ExpectedSalary { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set;} = String.Empty;
        public string CellPhone { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Career { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string PhotoUrl { get; set; } = String.Empty;
        public string ColorHex { get; set; } = String.Empty;
        public int Pages { get; set; }
    }
}
