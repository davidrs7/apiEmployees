using System;
namespace Api.DTO.Job
{
    public class JobSkillsDTO
    {
        public int Id { get; set; }
        public int Available { get; set; }
        public string Name { get; set; } = String.Empty;
        public string FieldType { get; set; } = String.Empty;
        public int IsRequired { get; set; }
    }
}
