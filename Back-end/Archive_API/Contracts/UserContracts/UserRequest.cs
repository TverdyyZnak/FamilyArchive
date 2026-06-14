namespace Archive_API.Contracts.UserContracts
{
    public record UserRequest(string login, string password, string email)
    {
    }
}
