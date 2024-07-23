using Api.DTO.Postulate;

namespace Api.Interfaces
{
    public interface IPostulateRepository
    {
        Task<IEnumerable<PostulateBasicDTO>> Postulates();
        Task<IEnumerable<PostulateBasicDTO>> Postulates(PostulateCriteriaDTO criteria);
        Task<PostulateDTO> PostulateById(int id);
        Task Edit(PostulateMergeDTO postulateEdit);
        Task<int> Add(PostulateMergeDTO postulateAdd);
        Task ToEmployee(PostulateToEmployeeDTO toEmployee);
    }
    
}