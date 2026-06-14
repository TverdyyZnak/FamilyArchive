using Domain.Classes;

namespace Application.Services
{
    public interface IUserService
    {
        Task<Guid> DeleteUser(Guid id);
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(Guid id);
        Task<User?> GetUserByLogin(string login);
        Task<string> Login(string login, string password);
        string Loguot();
        Task<Guid> Registration(User user);
    }
}