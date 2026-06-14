using Application.Services;
using Archive_API.Contracts.UserContracts;
using Microsoft.AspNetCore.Mvc;

namespace Archive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService userService)
        {
            _service = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetAllUsers()
        {
            return Ok(await _service.GetAllUsers());
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
        {
            var user = await _service.GetUserById(id);
            if (user == null) { return BadRequest("Пользователя с данным id не существует"); }
            return Ok(new UserResponse(user.Id, user.Login, user.Password, user.Email));
        }

        [HttpGet("by-login")]
        public async Task<ActionResult<UserResponse>> GetUserByLogin(string login)
        {
            var user = await _service.GetUserByLogin(login);
            if (user == null) { return BadRequest("Пользователя с данным логином не существует"); }
            return Ok(new UserResponse(user.Id, user.Login, user.Password, user.Email));
        }

        [HttpPost("reg")]
        public async Task<ActionResult<string>> Registration([FromBody] UserRequest request)
        {
            var user = Domain.Classes.User.Create(Guid.NewGuid(), request.login, request.password, request.email).user;
            var resp = await _service.Registration(user);
            if(resp == Guid.Empty) { return BadRequest("Пользователь с данным логином уже существует"); }
            return Ok(resp);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string login, string password)
        {
            string resp = await _service.Login(login, password);
            if (resp == "Неверный логин или пароль") { return BadRequest("Неверный логин или пароль"); }
            return Ok();
        }

        [HttpPost("logout")]
        public ActionResult<string> Logout()
        {
            return Ok(_service.Loguot());
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteUser(Guid id)
        {
            return Ok(await _service.DeleteUser(id));
        }
    }
}
