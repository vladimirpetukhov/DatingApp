using Microsoft.AspNetCore.Mvc;

namespace DateApp.API.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class UserController : ControllerBase {
        public UserController(IUserRepository)
        {
            
        }
    }
}