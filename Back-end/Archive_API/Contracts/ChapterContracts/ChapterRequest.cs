namespace Archive_API.Contracts.ChapterContracts
{
    public record ChapterRequest(int serial, string title, string description, DateOnly? start, DateOnly? end)
    {
    }
}
