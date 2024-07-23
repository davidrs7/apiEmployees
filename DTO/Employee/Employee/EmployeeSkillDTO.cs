namespace Api.DTO.Employee
{
    public class EmployeeSkillDTO
    {
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; } = String.Empty;
        public Boolean Active { get; set; }
        public int Rate { get; set; }
        public Boolean Inserted { get; set; }
        public Boolean Updated { get; set; }
    }
}