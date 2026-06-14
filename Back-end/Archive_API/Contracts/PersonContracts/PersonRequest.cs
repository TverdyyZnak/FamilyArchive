namespace Archive_API.Contracts.PersonContracts
{
    public record PersonRequest(string firstName, string lastName, string surname, string biography, DateOnly? birthday, DateOnly? death)
    {
    }
}
