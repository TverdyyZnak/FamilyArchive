using Archive_API.Contracts.PersonContracts;
using Archive_API.Contracts.UserContracts;

namespace Archive_API.Contracts.TreeContracts
{
    public record TreeFullResponse(Guid id, string title, Guid mainUserId, List<UserResponse> users, List<PersonFullResponse> persons)
    {
    }
}
