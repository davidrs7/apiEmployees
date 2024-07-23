namespace Api.DTO.Step
{
    public class StepFieldRelValueDTO
    {
        public int Id { get; set; }
        public int StepId { get; set; }
        public int VacantId { get; set; }
        public int PostulateId { get; set; }
        public int Available { get; set; }
        public string Name { get; set; } = String.Empty;
        public string FieldType { get; set; } = String.Empty;
        public int IsRequired { get; set; }
        public string? Config { get; set; } = String.Empty;
        public int Active { get; set; }
        public int Weight { get; set; }
        public string FieldValue { get; set; } = String.Empty;
        public Boolean Inserted { get; set; }
        public Boolean Updated { get; set; }
    }
}