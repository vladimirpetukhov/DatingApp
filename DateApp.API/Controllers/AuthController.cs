namespace DateApp.API.Controllers {
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Models.Dtos;
    using Models;

    [ApiController]
    [Route ("[controller]")]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController (IAuthRepository repo, IConfiguration configuration) {
            this._repo = repo;
            this._config = configuration;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (UserRegisterDto dto) {

            try {
                if (string.IsNullOrEmpty (dto.Username) || string.IsNullOrEmpty (dto.Password)) {
                    throw new ArgumentException ($"null arguments");
                }
                if (await this._repo.UserExist (dto.Username) == true) return BadRequest ("User Already exist!");
                var userToCreate = new User {
                    Username = dto.Username
                };

                var createdUser = this._repo.Register (userToCreate, dto.Password);
                if (createdUser == null) {
                    throw new InvalidOperationException ("Exeption in Register");
                }

            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }

            return StatusCode (201);
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login (UserLoginDto dto) {
            var userFromRepo = await _repo.Login (dto.Username, dto.Password);
            if (userFromRepo == null) return Unauthorized ();

            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, userFromRepo.Id.ToString ()),
                new Claim (ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey (Encoding
                .UTF8
                .GetBytes (_config.GetSection ("AppSettings:Token").Value));


            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();
            var token = tokenHandler.CreateToken (tokenDescriptor);

            return Ok (new {
                token = tokenHandler.WriteToken (token)
            });
        }
    }
}