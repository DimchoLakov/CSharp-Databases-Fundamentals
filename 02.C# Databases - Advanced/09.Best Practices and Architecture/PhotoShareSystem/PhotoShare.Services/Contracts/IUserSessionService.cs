using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IUserSessionService
    {
        User User { get; }
        
        bool IsLoggedIn { get; }

        void Login(string username);

        void Logout();
    }
}
