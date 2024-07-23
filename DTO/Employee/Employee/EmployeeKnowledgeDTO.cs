namespace Api.DTO.Employee
{
    public class EmployeeKnowledgeDTO
    {
        public int EmployeeId { get; set; }
        public int KnowledgeId { get; set; }
        public string KnowledgeName { get; set; } = String.Empty;
        public Boolean Active { get; set; }
        public int Rate { get; set; }
        public Boolean Inserted { get; set; }
        public Boolean Updated { get; set; }
    }
}