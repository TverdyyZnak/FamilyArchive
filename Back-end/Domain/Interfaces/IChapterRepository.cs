using Domain.Classes;

namespace Archive_DbContext.Repositories
{
    public interface IChapterRepository
    {
        Task<Guid> AddToFile(Guid id, FileResource file);
        Task<Guid> CreateNewChapter(Chapter chapter);
        Task<Guid> Delete(Guid id);
        Task<List<Chapter>> GetAll();
        Task<Chapter?> GetById(Guid id);
        Task<Guid> RemoveFile(Guid chapterId, Guid fileId);
        Task<Guid> Update(Guid id, int serial, string title, string description, DateOnly? start, DateOnly? end);
    }
}