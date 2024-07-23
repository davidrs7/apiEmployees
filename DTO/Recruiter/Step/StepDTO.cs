namespace Api.DTO.Step
{
    public class StepDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Available { get; set; }
        public string Description { get; set; } = String.Empty;
    }
}