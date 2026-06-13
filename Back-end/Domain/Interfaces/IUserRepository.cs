using Domain.Classes;

namespace Archive_DbContext.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> CreateNewUser(User user);
        Task<Guid> Delete(Guid id);
        Task<List<User>> GetAll();
        Task<User?> GetById(Guid id);
    }
}