using Archive_DbContext.Entities;
using Archive_DbContext.Repositories;
using Domain.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FamilyTreeService
    {
        private readonly IFamilyTreeRepository _repository;
        public FamilyTreeService(IFamilyTreeRepository familyTreeRepository)
        {
            _repository = familyTreeRepository;
        }

        public async Task<List<FamilyTree>> GetAllTrees()
        {
            return await _repository.GetAll();
        }

        public async Task<FamilyTree?> GetTreeById(Guid id)
        {
            return await _repository.GetTreeById(id);
        }

        public async Task<List<FamilyTree>> GetTreesByUserId(Guid userId)
        {
            return await _repository.GetByUserId(userId);
        }

        public async Task<Guid> UpdateTreeTitle(Guid id, string title)
        {
            return await _repository.UpdateTitle(id, title);
        }

        public async Task<Guid> CreateNewTree(FamilyTree tree)
        {
            return await _repository.CreateNewTree(tree);
        }

        public async Task<Guid> AddUserToTreeUserList(Guid treeId, Guid userId)
        {
            return await _repository.AddUser(treeId, userId);
        }

        public async Task<Guid> DeleteTree(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
