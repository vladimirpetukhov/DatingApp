namespace DateApp.API.Data
{
    using Models;
    using System.Threading.Tasks;
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
}