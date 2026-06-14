using Archive_API.Contracts.FileContracts;

namespace Archive_API.Contracts.ChapterContracts
{
    public record ChapterResponse(Guid id, int serial, string title, string description, DateOnly? start, DateOnly? end, List<FileResponse> files)
    {
    }
}
