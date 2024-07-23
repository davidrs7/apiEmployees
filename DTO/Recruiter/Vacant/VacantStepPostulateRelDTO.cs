namespace Api.DTO.Vacant
{
    public class VacantStepPostulateRelDTO
    {
        public int Id { get; set; }
        public int VacantId { get; set; }
        public int PostulateId { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Available { get; set; }
        public string Description { get; set; } = String.Empty;
        public int Active { get; set; }
        public int Weight { get; set; }
        public int IsRequired { get; set; }
        public int Approved { get; set; }
        public string Reason { get; set; } = String.Empty;
        public Boolean Inserted { get; set; }
        public Boolean Updated { get; set; }
    }
}