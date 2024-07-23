using Api.DTO.Absence;

namespace Api.Interfaces
{
    public interface IAbsenceRepository
    {
        Task<IEnumerable<AbsenceUserDTO>> Users(int userId);
        Task<AbsenceUserDTO> User(int employeeId);
        Task<IEnumerable<AbsenceDTO>> Absences(int employeeId);
        Task<AbsenceDTO> Absence(int Id);
        Task<int> Add(AbsenceDTO absenceAdd);
        Task Edit(AbsenceDTO absenceEdit);
        Task<IEnumerable<AbsenceApprovalDTO>> Approvals(int absenceId);
        Task<int> AddApproval(AbsenceApprovalDTO approvalAdd);
        Task EditApproval(AbsenceApprovalDTO approvalEdit);
        Task<IEnumerable<AbsenceFileDTO>> Files(int absenceId);
        Task<int> AddFile(AbsenceFileDTO fileDTO);
    }
}