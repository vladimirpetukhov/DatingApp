namespace DateApp.API.Controllers
{
    using Data;
    using Models;
    using Models.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;


    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {

            try
            {
                if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
                {
                    throw new ArgumentException($"null arguments");
                }
                if (await this._repo.UserExist(dto.Username) == true) return BadRequest("User Already exist!");
                var userToCreate = new User
                {
                    Username = dto.Username
                };

                var createdUser = this._repo.Register(userToCreate, dto.Password);
                if (createdUser == null)
                {
                    throw new InvalidOperationException("Exeption in Register");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(201);
        }
    }
}