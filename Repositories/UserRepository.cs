using System.Data.SqlClient;
using Api.DTO.User;
using Api.Interfaces;
using Api.Queries;
using Dapper;
using MySql;
using MySqlConnector;

namespace Api.Repositories
{
    public class UserRepository: IUserRepository
    {
        private IConfiguration _configuration;
        private UserQueries _userQueries;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _userQueries = new UserQueries();
        }

        public async Task<Boolean> Login(UserDTO user)
        {
            string userSql = _userQueries.UserLogin;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var userResponse = await connection.QueryAsync<UserDTO>(userSql, new {
                    UserName = user.UserName,
                    Password = user.Password
                });
                return userResponse.Count() > 0;
            }
        }

        public async Task<SessionDTO> CreateSession(UserDTO user) {
            string userSql = _userQueries.UserId;
            string sessionSql = _userQueries.AddSession;
            int userId = 0;

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand commandUser = new MySqlCommand(userSql, connection))
                {
                    await connection.OpenAsync();
                    commandUser.Parameters.Add(new MySqlParameter("UserName", user.UserName));
                    userId = Convert.ToInt32(await commandUser.ExecuteScalarAsync());

                    SessionDTO session = new SessionDTO();
                    session.UserId = userId;
                    session.Created = DateTime.Now;
                    session.Reload = DateTime.Now;
                    session.IpAddress = System.Net.Dns.GetHostName();
                    session.Token = this.ComposeHashToken(user.UserName, session.Created);

                    var sessionResponse = await connection.ExecuteAsync(sessionSql, new {
                        Token = session.Token,
                        UserId = session.UserId,
                        Created = session.Created,
                        Reload = session.Reload,
                        IpAddress = session.IpAddress
                    });
                    return session;
                }
            }
        }

        public async Task<Boolean> DestroySession(string token) {
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await this.DestroySession(connection, token);
                return true;
            }
        }

        public async Task<UserDTO> UserByToken(string token)
        {
            string sessionSql = _userQueries.SessionByToken;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            { 
                    await connection.OpenAsync();
                    var sessionResponse = await connection.QueryAsync<SessionDTO>(sessionSql, new
                    {
                        Token = token
                    });
                    SessionDTO session = sessionResponse.Count() > 0 ? sessionResponse.First() : null;

                    if (session != null)
                    {
                        DateTime dateValidate = DateTime.Now.AddHours(-2);
                        if (DateTime.Compare(session.Reload, dateValidate) < 0)
                        {
                            await this.DestroySession(connection, token);
                        }
                        else
                        {
                            string userSql = _userQueries.UserById;
                            var userResponse = await connection.QueryAsync<UserDTO>(userSql, new
                            {
                                Id = session.UserId
                            });
                            return userResponse.First();
                        }
                    }
                    return null;
                 
                
            }
        }


        public async Task<UserDTO> UserByIdOpcional(int userIdOpcional)
        {
            string sessionSql = _userQueries.userByIdOpcional;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            { 
                    await connection.OpenAsync();    
                            string userSql = _userQueries.userByIdOpcional;
                            var userResponse = await connection.QueryAsync<UserDTO>(userSql, new
                            {
                                Id = userIdOpcional
                            });
                            return userResponse.First();  
                    return null;
 

            }
        }

        public async Task<IEnumerable<UserDTO>> Users()
        {
            string usersSql = _userQueries.Users;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var usersResponse = await connection.QueryAsync<UserDTO>(usersSql);
                return usersResponse.ToList();
            }
        }

        private async Task<int> DestroySession(MySqlConnection connection, string token) {
            return await connection.ExecuteAsync(_userQueries.DeleteSession, new { Token = token });
        }

        private string ComposeHashToken(string userName, DateTime created) {
            string token = userName + "TOKEN" + created.ToString();
            using (var crypt = System.Security.Cryptography.SHA256.Create())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(token);
                byte[] hashBytes = crypt.ComputeHash(textBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }
    }
}