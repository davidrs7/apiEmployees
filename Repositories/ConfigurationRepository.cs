using System.Data.SqlClient;
using Api.DTO.Configuration;
using Api.Interfaces;
using Api.Queries;
using Api.Utils;
using Dapper;
using MySql.Data.MySqlClient;

namespace Api.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private IConfiguration _configuration;
        private ConfigurationQueries _configurationQueries;
        public ConfigurationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _configurationQueries = new ConfigurationQueries();
        }

        public async Task<IEnumerable<ConfigurationItemDTO>> ConfigurationItems(string configKey)
        {
            string configSql = _configurationQueries.ConfigurationItems;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var configResponse = await connection.QueryAsync<ConfigurationItemDTO>(configSql, new {
                    ConfigKey = configKey
                });
                return configResponse.ToList();
            }
        }

        public async Task MergeConfiguration(ConfigurationDTO configDTO)
        {
            string searchSql = _configurationQueries.SearchConfig;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(searchSql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("ConfigKey", configDTO.ConfigKey));
                    await connection.OpenAsync();
                    int countConfig = Convert.ToInt32(await command.ExecuteScalarAsync());
                    string configSql = (countConfig > 0) ? _configurationQueries.ConfigEdit : _configurationQueries.ConfigAdd;

                    var configResponse = await connection.ExecuteAsync(configSql, new {
                        ConfigKey = configDTO.ConfigKey,
                        UserId = configDTO.UserId,
                        Updated = DateTime.Now,
                        Value1 = configDTO.Value1,
                        Value2 = configDTO.Value2,
                        ListType = configDTO.ListType
                    });
                }
            }
        }

        public async Task SetConfigurationItem(ConfigurationItemDTO itemDTO)
        {
            string configSql = _configurationQueries.ItemAdd;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var configResponse = await connection.ExecuteAsync(configSql, new {
                    ConfigKey = itemDTO.ConfigKey,
                    Item = itemDTO.ListItem,
                    Value = itemDTO.ListValue
                });
            }
        }

        public async Task DeleteConfigurationItem(ConfigurationItemDTO itemDTO)
        {
            string configSql = _configurationQueries.ItemDelete;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var configResponse = await connection.ExecuteAsync(configSql, new {
                    ConfigKey = itemDTO.ConfigKey,
                    Item = itemDTO.ListItem,
                    Value = itemDTO.ListValue
                });
            }
        }

        public async Task<IEnumerable<ConfigurationUserDTO>> UsersByAbsenceTypesAllowed()
        {
            string configSql = _configurationQueries.ConfigurationItems;
            string usersSql = _configurationQueries.UsersByDepartments;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var configResponse = await connection.QueryAsync<ConfigurationItemDTO>(configSql, new {
                    ConfigKey = "absence.types.allowed"
                });

                if(configResponse.Count() > 0) {
                    var configObj = configResponse.First();
                    if(configObj.Value2 != "" && configObj.Value2 != "0") {
                        var configUsersResponse = await connection.QueryAsync<ConfigurationItemDTO>(configSql, new {
                            ConfigKey = "absence.users.allowed"
                        });

                        string countIns = "0";
                        if(configUsersResponse.Count() > 0) {
                            foreach(ConfigurationItemDTO configItem in configUsersResponse.ToList())
                                if(configItem.ListItem != null && configItem.ListItem != "")
                                    countIns += "," + configItem.ListItem;
                            countIns = countIns.Replace("0,","");
                        }

                        var usersResponse = await connection.QueryAsync<ConfigurationUserDTO>(
                            usersSql.Replace("@Ids", configObj.Value2).Replace("@Ins", countIns)
                        );
                        return usersResponse.ToList();
                    }
                }

                return new List<ConfigurationUserDTO>();
            }
        }
    }
}