using Domain.Classes;

namespace Application.Services
{
    public interface IFamilyTreeService
    {
        Task<Guid> AddUserToTreeUserList(Guid treeId, Guid userId);
        Task<Guid> CreateNewTree(FamilyTree tree);
        Task<Guid> DeleteTree(Guid id);
        Task<List<FamilyTree>> GetAllTrees();
        Task<FamilyTree?> GetTreeById(Guid id);
        Task<List<FamilyTree>> GetTreesByUserId(Guid userId);
        Task<Guid> UpdateTreeTitle(Guid id, string title);
    }
}