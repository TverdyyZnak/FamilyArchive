using Domain.Classes;

namespace Archive_DbContext.Repositories
{
    public interface IFamilyTreeRepository
    {
        Task<Guid> AddUser(Guid treeId, Guid userId);
        Task<Guid> CreateNewTree(FamilyTree tree);
        Task<Guid> Delete(Guid id);
        Task<List<FamilyTree>> GetAll();
        Task<List<FamilyTree>> GetByUserId(Guid userId);
        Task<FamilyTree?> GetTreeById(Guid id);
        Task<Guid> UpdateTitle(Guid id, string title);
    }
}