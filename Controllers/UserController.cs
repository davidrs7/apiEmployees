using Api.DTO.User;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController: ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Boolean>> Login([FromForm] UserDTO user)
        {
            return Ok(await _userRepository.Login(user));
        }

        [HttpPost("Session/Create")]
        public async Task<ActionResult<SessionDTO>> CreateSession([FromForm] UserDTO user)
        {
            return Ok(await _userRepository.CreateSession(user));
        }

        [HttpGet("Session/Destroy/{token}")]
        public async Task<ActionResult<Boolean>> DestroySession(string token)
        {
            return Ok(await _userRepository.DestroySession(token));
        }

        [HttpGet("User/{token}")]
        public async Task<ActionResult<UserDTO>> UserByToken(string token)
        {
            return Ok(await _userRepository.UserByToken(token));
        }

        [HttpGet("UserEmployee/{userIdOpcional}")]
        public async Task<ActionResult<UserDTO>> UserByIdOpcional(int userIdOpcional)
        {
            return Ok(await _userRepository.UserByIdOpcional(userIdOpcional));
        }

        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Users()
        {
            return Ok(await _userRepository.Users());
        }
    }
}