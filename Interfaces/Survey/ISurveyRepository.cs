using Api.DTO.Survey;

namespace Api.Interfaces
{
    public interface ISurveyRepository
    {
        Task<IEnumerable<SurveyDTO>> Surveys();
        Task<SurveyDTO> SurveyById(int Id);
        Task<int> Add(SurveyDTO surveyAdd);
        Task Edit(SurveyDTO surveyEdit);
        Task<IEnumerable<SurveyHeaderDTO>> HeadersBySurvey(int surveyId);
        Task<IEnumerable<SurveyHeaderDTO>> HeadersByUser(int userId);
        Task<SurveyHeaderDTO> HeaderBySurveyAndUser(int surveyId, int userId);
        Task<int> AddHeader(SurveyHeaderDTO surveyAdd);
        Task EditHeader(SurveyHeaderDTO surveyEdit);
        Task<IEnumerable<SurveyFieldDTO>> Fields();
        Task<SurveyFieldDTO> FieldById(int Id);
        Task<IEnumerable<SurveyFieldDTO>> FieldsBySurvey(int surveyId);
        Task<IEnumerable<SurveyFieldDTO>> FieldsByHeader(int headerId);
        Task AddField(SurveyFieldDTO fieldAdd);
        Task EditField(SurveyFieldDTO fieldEdit);
        Task MergeSurveyFieldRel(SurveyFieldRelDTO relMerge);
        Task<IEnumerable<SurveyUserDTO>> UsersByHeader(int headerId);
        Task<int> AddUserRel(SurveyUserRelDTO userRel);
        Task<int> AddUserRelByDepartment(SurveyUserRelDTO userRel);
        Task<int> AddUserRelByJob(SurveyUserRelDTO userRel);
        Task<int> AddUserRelByCity(SurveyUserRelDTO userRel);
        Task<int> DeleteUserRel(SurveyUserRelDTO userRel);
    }
}
