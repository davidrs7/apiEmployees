using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.Employee;

namespace Api.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeBasicDTO>> Employees(EmployeeCriteriaDTO employeeCriteria);
        Task<IEnumerable<EmployeeBasicDTO>> EmployeesWithoutPages(int excludeEmployeeId);
        Task<IEnumerable<EmployeeDownloadDTO>> EmployeesDownload(EmployeeCriteriaDTO employeeCriteria);
        Task<IEnumerable<EmployeeSkillDTO>> Skills();
        Task<IEnumerable<EmployeeKnowledgeDTO>> Knowledges();
        Task<IEnumerable<EmployeeSkillDTO>> Skills(int employeeId);
        Task<IEnumerable<EmployeeKnowledgeDTO>> Knowledges(int employeeId);
        Task<IEnumerable<EmployeeFileDTO>> Files(int employeeId);
        Task<EmployeeDTO> Employee(int employeeId);
        Task<EmployeeGeneralDTO> EmployeeGeneral(int employeeId);
        Task<EmployeeAcademicDTO> EmployeeAcademic(int employeeId);
        Task<IEnumerable<EmployeeFileTypeDTO>> EmployeeFileTypes();
        Task<int> Add(EmployeeMergeDTO employeeAdd);
        Task<int> AddGeneral(EmployeeGeneralDTO employeeGeneralAdd);
        Task<int> AddAcademic(EmployeeAcademicDTO employeeAcademicAdd);
        Task<int> AddFile(EmployeeFileMergeDTO employeeFileAdd);
        Task Edit(EmployeeMergeDTO employeeEdit);
        Task EditGeneral(EmployeeGeneralDTO employeeGeneralEdit);
        Task EditAcademic(EmployeeAcademicDTO employeeAcademicEdit);
        Task MergeKnowledge(EmployeeKnowledgeDTO merge);
        Task MergeSkill(EmployeeSkillDTO merge);
        Task<IEnumerable<EmployeeSonsDTO>> Sons(int employeeId);
        Task<int> UpdateSons(EmployeeSonsDTO sonData);
        Task<int> AddSons(EmployeeSonsDTO sonData);
        Task<int> DelSons(int sonId);
    }
}