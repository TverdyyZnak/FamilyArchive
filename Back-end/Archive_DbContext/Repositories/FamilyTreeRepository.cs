using Archive_DbContext.Entities;
using Domain.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Repositories
{
    public class FamilyTreeRepository : IFamilyTreeRepository
    {
        private readonly AppDbContext _context;
        public FamilyTreeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<FamilyTree>> GetAll()
        {
            var treeEntity = await _context.FamilyTree.AsNoTracking().ToListAsync();
            var tree = treeEntity.Select(t => FamilyTree.Create(t.Id, t.Title, t.MainUserId).tree).ToList();
            return tree;
        }

        public async Task<FamilyTree?> GetTreeById(Guid id)
        {
            var te = await _context.FamilyTree.Include(t => t.Users)
                .Include(t => t.Persons)
                .ThenInclude(p => p.Chapters)
                .ThenInclude(c => c.Files)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (te == null) { return null; }

            var tree = FamilyTree.Create(te.Id, te.Title, te.MainUserId).tree;

            tree.Users = te.Users.Select(u => User.Create(u.Id, u.Login, u.Password, u.Email).user).ToList();
            tree.Persons = te.Persons.Select(p =>
            {
                var person = Person.Create(p.Id, p.FirstName, p.LastName, p.Surname, p.ShortBiography, p.Birthday, p.DayOfDeath, p.FatherId, p.MotherId).person;

                person.Chapters = p.Chapters.Select(c =>
                {
                    var chapter = Chapter.Create(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate).chapter;
                    chapter.Files = c.Files.Select(f => FileResource.Create(f.Id, f.Title, f.ResourceUrl).file).ToList();
                    return chapter;
                }).ToList();

                return person;
            }).ToList();

            return tree;
        }

        public async Task<List<FamilyTree>> GetByUserId(Guid userId)
        {
            var treeEntities = await _context.FamilyTree.Where(t => t.MainUserId == userId).AsNoTracking().ToListAsync();
            var trees = treeEntities.Select(t => FamilyTree.Create(t.Id, t.Title, t.MainUserId).tree).ToList();
            return trees;
        }

        public async Task<Guid> UpdateTitle(Guid id, string title)
        {
            int rowCount = await _context.FamilyTree.Where(t => t.Id == id).ExecuteUpdateAsync(o => o.SetProperty(t => t.Title, t => title));
            if (rowCount == 0) { return Guid.Empty; }
            return id;
        }

        public async Task<Guid> CreateNewTree(FamilyTree tree)
        {
            FamilyTreeEntity entity = new FamilyTreeEntity
            {
                Id = tree.Id,
                Title = tree.Title,
                MainUserId = tree.MainUserId
            };

            await _context.FamilyTree.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> AddUser(Guid treeId, Guid userId)
        {
            var tree = await _context.FamilyTree.FirstOrDefaultAsync(t => t.Id == treeId);
            if (tree == null) { return Guid.Empty; }

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) { return Guid.Empty; }

            if (!tree.Users.Contains(user))
            {
                tree.Users.Add(user);
                await _context.SaveChangesAsync();
                return tree.Id;
            }
            else
            {
                return tree.Id;
            }
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.FamilyTree.Where(t => t.Id == id).ExecuteDeleteAsync();
            return id;
        }
    }
}
