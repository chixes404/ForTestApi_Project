using fotTestAPI.Model;

namespace fotTestAPI.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);


    }
}
