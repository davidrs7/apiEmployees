
namespace Api.Queries
{
    public class ConfigurationQueries
    {
        public string ConfigurationItems { get; } = "SELECT C.ConfigKey, C.UserId, C.Updated, C.Value1, C.Value2, C.ListType, CL.Item AS ListItem, CL.Value AS ListValue FROM configuration C LEFT JOIN configuration_list CL ON C.ConfigKey = CL.ConfigKey WHERE C.ConfigKey = @ConfigKey";
        public string ConfigurationItemValue { get; } = "SELECT C.ConfigKey, C.UserId, C.Updated, C.Value1, C.Value2, C.ListType, CL.Item AS ListItem, CL.Value AS ListValue FROM configuration C INNER JOIN configuration_list CL ON C.ConfigKey = CL.ConfigKey WHERE C.ConfigKey = @ConfigKey AND CL.Value = @Value";
        public string ItemAdd { get; } = "INSERT INTO configuration_list (ConfigKey, Item, Value) VALUES (@ConfigKey, @Item, @Value)";
        public string ItemDelete { get; } = "DELETE FROM configuration_list WHERE ConfigKey = @ConfigKey AND Item = @Item AND Value = @Value";
        public string SearchConfig { get; } = "SELECT COUNT(*) FROM configuration C WHERE C.ConfigKey = @ConfigKey";
        public string ConfigAdd { get; } = "INSERT INTO configuration (ConfigKey, UserId, Updated, Value1, Value2, ListType) VALUES (@ConfigKey, @UserId, @Updated, @Value1, @Value2, @ListType)";
        public string ConfigEdit { get; } = "UPDATE configuration SET UserId = @UserId, Updated = @Updated, Value1 = @Value1, Value2 = @Value2, ListType = @ListType WHERE ConfigKey = @ConfigKey";
        public string UsersByDepartments { get; } = "SELECT U.Id, U.UserName, U.EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, E.JobId, J.Name AS JobName, J.DepartmentId, D.Name AS DepartmentName, IF(U.Id IN (@Ins), 1, 0) AS IsSelected FROM user_login U INNER JOIN employee E ON U.EmployeeId = E.Id INNER JOIN job J ON E.JobId = J.Id INNER JOIN department D ON J.DepartmentId = D.Id WHERE D.Id IN (@Ids)";
    }
}