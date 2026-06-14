namespace Archive_API.Contracts.PersonContracts
{
    public record PersonShortResponse(Guid id, string firstName, string lastName, string surname, string biography, DateOnly? birthday, DateOnly? death)
    {
    }
}
