using My_Resturant.DTOs.Auth;

namespace My_Resturant.Interfaces
{
    public interface IAuthServices
    {
        Task<string> LogIn(LogInDTOs logInDTOs);
        Task<bool> LogOut(string token);
        Task<bool>IsTokenInBlackList(string token);

    }
}
