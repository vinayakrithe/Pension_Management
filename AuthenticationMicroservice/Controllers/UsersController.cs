using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Controllers
{
    [Authorize]
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UsersController));
        public UsersController(IUserRepository userRepo )
        {
            _userRepo = userRepo;
        }

   
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationModel model)
        {
            _log4net.Info(" Http POST Request From Authentication method of: " + nameof(UsersController));
            var user =await _userRepo.Authenticate(model.Username, model.Password);
            if (user == null)
            {
              
                _log4net.Warn(" Badrequest returned from Authentication method of: " + nameof(UsersController));
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            _log4net.Info(" Token successfully returned from Authentication method of: " + nameof(UsersController));
            return Ok(user);
        }

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterAsync([FromBody] RegistrationModel model)
        //{
        //    bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
        //    bool ifEmailUnique = _userRepo.IsUniqueEmail(model.Email);
        //    if (!ifUserNameUnique)
        //    {
        //        return BadRequest(new
        //        {
        //            errorMessage = "Username already exists"
        //        });
        //    }
        //    else if (!ifEmailUnique)
        //    {
        //        return BadRequest(new
        //        {
        //            errorMessage = "Email already exists"
        //        });
        //    }

        //    var user = await _userRepo.Register(model.Email, model.Username, model.Password);

        //    if (user == null)
        //    {
        //        return BadRequest(new
        //        {
        //            message = "Error while registering"
        //        });
        //    }

        //    return Ok();
        //}
    }
}
