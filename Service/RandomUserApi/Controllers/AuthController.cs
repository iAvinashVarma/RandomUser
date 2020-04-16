using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomUser.Business.Contract.Services;
using RandomUser.Business.Entity.Model;

namespace RandomUserApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public AuthController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]UserAccountModel model)
        {
            var user = _userAccountService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}