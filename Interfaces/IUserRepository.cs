using Api.DTO.User;

namespace Api.Interfaces
{
    public interface IUserRepository
    {
        Task<Boolean> Login(UserDTO user);
        Task<UserDTO> UserByToken(string token);
        Task<UserDTO> UserByIdOpcional(int userIdOpcional);
        Task<IEnumerable<UserDTO>> Users();
        Task<SessionDTO> CreateSession(UserDTO user);
        Task<Boolean> DestroySession(string token);
    }
}