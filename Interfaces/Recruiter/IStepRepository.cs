using Api.DTO.Step;

namespace Api.Interfaces
{
    public interface IStepRepository
    {
        Task<IEnumerable<StepDTO>> Steps();
        Task<StepDTO> StepById(int Id);
        Task<int> Add(StepDTO stepAdd);
        Task Edit(StepDTO stepEdit);
        Task<IEnumerable<StepFieldDTO>> Fields();
        Task<StepFieldDTO> FieldById(int Id);
        Task<IEnumerable<StepFieldRelDTO>> FieldsByStep(int stepId);
        Task<IEnumerable<StepFieldRelValueDTO>> FieldsByStepRel(int stepId, int vacantId, int postulateId);
        Task AddField(StepFieldDTO stepAdd);
        Task EditField(StepFieldDTO stepEdit);
        Task MergeStepFieldRel(StepFieldRelDTO relMerge);
        Task MergeStepFieldRelValue(StepFieldRelValueDTO relMerge);
    }
}