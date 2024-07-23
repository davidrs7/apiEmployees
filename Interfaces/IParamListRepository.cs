using Api.DTO.ParamList;

namespace Api.Interfaces
{
    public interface IParamListRepository
    {
        Task<IEnumerable<ParamListDTO>> DocTypeParams();
        Task<IEnumerable<ParamListDTO>> CityParams();
        Task<IEnumerable<ParamListDTO>> MaritalStatusParams();
        Task<IEnumerable<ParamListDTO>> HousingTypeParams();
        Task<IEnumerable<ParamListDTO>> EducationalLevelParams();
        Task<IEnumerable<ParamListDTO>> EmployeeStatusParams();
        Task<IEnumerable<ParamListDTO>> ContractTypeParams();
        Task<IEnumerable<ParamListDTO>> BankingEntityParams();
        Task<IEnumerable<ParamListDTO>> TransportationParams();
        Task<IEnumerable<ParamListDTO>> VacantStatusParams();
        Task<IEnumerable<ParamListDTO>> PostulateFindOutParams();
        Task<IEnumerable<ParamListDTO>> JobSkillsParams();
        Task<IEnumerable<ParamListDTO>> DepartmentParams();
        Task<IEnumerable<ParamListDTO>> JobParams();
        Task<IEnumerable<ParamListDTO>> AbsenceTypeParams();
    }
}