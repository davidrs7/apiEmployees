namespace Api.DTO.Vacant
{
    public class VacantStepDTO
    {
        public int Id { get; set; }
        public int VacantId { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Available { get; set; }
        public string Description { get; set; } = String.Empty;
        public int Active { get; set; }
        public int Weight { get; set; }
        public int IsRequired { get; set; }
        public Boolean Inserted { get; set; }
        public Boolean Updated { get; set; }
    }
}