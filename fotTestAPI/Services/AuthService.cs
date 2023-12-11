using AutoMapper;
using fotTestAPI.Configuration;
using fotTestAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fotTestAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper; // Inject IMapper
        private readonly Jwt _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<Jwt> jwt, IMapper mapper)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _mapper = mapper; // Initialize IMapper
        }

        /*
         *  registerModel ==> the form user will fill 
         *  AuthModel ===> the response data after submission
         *  ApplicationUser ==>
         * 
         * 
         * */


        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            // Check if the email is already registered
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email is already registered." };
            }

            // Check if the username is already registered
            if (await _userManager.FindByNameAsync(model.Username) is not null)
            {
                return new AuthModel { Message = "Username is already registered." };
            }

            // Use AutoMapper to map RegisterModel to ApplicationUser
            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User creation was successful, add the user to the "User" role
                await _userManager.AddToRoleAsync(user, "User");

                // Generate JWT token
                var jwtSecurityToken = await CreateJwtToken(user);

                return new AuthModel
                {
                    Email = user.Email,
                    Expireson = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "User" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Username = user.UserName
                };
            }
            else
            {
                // User creation failed, return AuthModel with failure information
                var errors = string.Join(", ", result.Errors.Select(error => error.Description));
                return new AuthModel { Message = errors };
            }
        }


        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
    }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
           }

        } 
    }
