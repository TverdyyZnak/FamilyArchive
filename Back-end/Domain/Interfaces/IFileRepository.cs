using Domain.Classes;

namespace Archive_DbContext.Repositories
{
    public interface IFileRepository
    {
        Task<Guid> CreateNewFile(FileResource file);
        Task<Guid> Delete(Guid id);
        Task<List<FileResource>> GetAll();
        Task<FileResource?> GetFileById(Guid id);
        Task<Guid> Update(Guid id, string title, string url);
    }
}