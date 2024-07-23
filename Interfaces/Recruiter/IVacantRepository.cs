using Api.DTO.Vacant;

namespace Api.Interfaces
{
    public interface IVacantRepository
    {
        Task<IEnumerable<VacantDTO>> Vacants();
        Task<VacantDTO> VacantById(int Id);
        Task<int> Add(VacantDTO stepAdd);
        Task Edit(VacantDTO stepEdit);
        Task<IEnumerable<VacantStepDTO>> VacantSteps(int Id);
        Task<IEnumerable<VacantStepPostulateRelDTO>> VacantStepsByPostulateRel(int vacantId, int postulateId);
        Task<IEnumerable<VacantPostulateRelDTO>> VacantsByPostulate(int postulateId);
        Task<IEnumerable<VacantPostulateRelDTO>> VacantsByVacantRel(int vacantId);
        Task MergeVacantStepRel(VacantStepDTO relMerge);
        Task MergeVacantStepPostulateRel(VacantStepPostulateRelDTO relMerge);
        Task<int> AddVacantPostulateRel(VacantPostulateRelDTO relMerge);
        Task<IEnumerable<VacantDTO>> HistoricalVacants();
        Task<VacantDTO> HistoricalVacantById(int Id);
    }
}