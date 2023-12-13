using fotTestAPI.Model;
using fotTestAPI.Model.Authentication;

namespace fotTestAPI.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);
		Task<AuthModel> GetTokenAsync(TokenRequestModel model);
		Task<string> AddRoleAsync(AddRoleModel model);

	}
}
