using Domain.Classes;

namespace Application.Services
{
    public interface IFileService
    {
        Task<Guid> CreateNewFileResource(FileResource file);
        Task<Guid> DeleteFile(Guid id);
        Task<List<FileResource>> GetAllFiles();
        Task<FileResource?> GetFileById(Guid id);
        Task<Guid> UpdateFile(Guid id, string title, string url);
    }
}