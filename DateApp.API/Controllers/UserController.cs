namespace DateApp.API.Controllers {
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route ("[controller]")]
    public class UserController : ControllerBase {

        private readonly IDatingRepository _dateRepo;
        private readonly IMapper _mapper;
        public UserController (IDatingRepository datingRepository, IMapper mapper) {
            this._dateRepo = datingRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser (int id) {
            var user = await _dateRepo.GetUser (id);
            return Ok (user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers () {
            var users = await _dateRepo.GetUsers ();
            return Ok (users);
        }
    }
}