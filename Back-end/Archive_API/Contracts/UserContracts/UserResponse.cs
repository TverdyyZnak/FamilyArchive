namespace Archive_API.Contracts.UserContracts
{
    public record UserResponse(Guid id, string login, string password, string email)
    {
    }
}
