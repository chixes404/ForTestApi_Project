using fotTestAPI.Model;
using fotTestAPI.Model.Authentication;
using fotTestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Protocol;

namespace fotTestAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
		private readonly UserManager<ApplicationUser> _userManager;

		[ActivatorUtilitiesConstructor]
		public AuthController(IAuthService authService , UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
			_userManager = userManager;	 
        }





        [HttpPost("register")]
          public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(new { Token = result.Token , ExpireDate = result.Expireson});  //anonymus object
        }

		[HttpGet("getUserByname")]  //this action didn't exist in Services
		public async Task<IActionResult> GetUserByUsername(string username)
		{
			// Check if the user exists
			var user = await _userManager.FindByNameAsync(username);

			if (user == null)
			{
				return NotFound("User not found");
			}

			// You may want to customize the data you return based on your requirements
			var userData = new
			{
				UserId = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Token = user.ToJToken()
				// Add any other properties you want to expose
			};

			return Ok(userData);
		}


		[HttpPost("token")]
		public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.GetTokenAsync(model);

			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok(result);
		}




		[HttpPost("addrole")]
		public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.AddRoleAsync(model);

			if (!string.IsNullOrEmpty(result))
				return BadRequest(result);

			return Ok(model);
		}



	}
}

   