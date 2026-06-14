using Archive_API.Contracts.ChapterContracts;

namespace Archive_API.Contracts.PersonContracts
{
    public record PersonFullResponse(Guid id, string firstName, string lastName, string surname, string biography, DateOnly? birthday, DateOnly? death, List<ChapterResponse> chapters)
    {
    }
}
