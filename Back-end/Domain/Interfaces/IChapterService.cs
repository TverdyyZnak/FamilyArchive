using Domain.Classes;

namespace Application.Services
{
    public interface IChapterService
    {
        Task<Guid> AddFileToChapter(Guid id, FileResource file);
        Task<Guid> CreateNewChapter(Chapter chapter);
        Task<Guid> DeleteChapter(Guid id);
        Task<List<Chapter>> GetAllChapters();
        Task<Chapter?> GetChapterById(Guid id);
        Task<Guid> RemoveFileFromChapter(Guid chapterId, Guid fileId);
        Task<Guid> UpdateChapter(Guid id, int serial, string title, string description, DateOnly? start, DateOnly? end);
    }
}