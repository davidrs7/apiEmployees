namespace Api.DTO.Postulate
{
    public class PostulateCriteriaDTO
    {
        public int? VacantId { get; set; }
        public string? Doc { get; set; } = String.Empty;
        public string? Name { get; set; } = String.Empty;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public decimal Pages { get; set; }
        public bool ActivePaginator { get; set; }
    }
}