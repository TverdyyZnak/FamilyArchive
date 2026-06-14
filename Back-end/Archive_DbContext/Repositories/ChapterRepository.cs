using Archive_DbContext.Entities;
using Domain.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly AppDbContext _context;
        public ChapterRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Chapter>> GetAll()
        {
            var chapterEntities = await _context.Chapter.Include(c => c.Files).ToListAsync();
            var resp = chapterEntities.Select(c =>
            {
                var ch = Chapter.Create(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate).chapter;
                ch.Files = c.Files.Select(f => FileResource.Create(f.Id, f.Title, f.ResourceUrl).file).ToList();
                return ch;
            }).ToList();

            return resp;
        }

        public async Task<Chapter?> GetById(Guid id)
        {
            var ch = await _context.Chapter.Include(c => c.Files).FirstOrDefaultAsync(c => c.Id == id);
            if (ch == null) { return null; }
            var resp = Chapter.Create(ch.Id, ch.SerialNumber, ch.Title, ch.Description, ch.StartDate, ch.EndDate).chapter;
            resp.Files = ch.Files.Select(f => FileResource.Create(f.Id, f.Title, f.ResourceUrl).file).ToList();
            return resp;
        }

        public async Task<Guid> CreateNewChapter(Chapter chapter)
        {
            ChapterEntity entity = new ChapterEntity
            {
                Id = chapter.Id,
                SerialNumber = chapter.SerialNumber,
                Title = chapter.Title,
                Description = chapter.Description,
                StartDate = chapter.StartDate,
                EndDate = chapter.EndDate,
            };

            await _context.Chapter.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> Update(Guid id, int serial, string title, string description, DateOnly? start, DateOnly? end)
        {
            int rowCount = await _context.Chapter.Where(f => f.Id == id).ExecuteUpdateAsync(o => o
            .SetProperty(f => f.SerialNumber, f => serial)
            .SetProperty(f => f.Title, f => title)
            .SetProperty(f => f.Description, f => description)
            .SetProperty(f => f.StartDate, f => start)
            .SetProperty(f => f.EndDate, f => end));

            if (rowCount == 0) { return Guid.Empty; }
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Chapter.Include(f => f.Files).Where(f => f.Id == id).ExecuteDeleteAsync();
            return id;
        }

        public async Task<Guid> AddToFile(Guid id, FileResource file)
        {
            var chapter = await _context.Chapter.FirstOrDefaultAsync(c => c.Id == id);
            if (chapter == null) { return Guid.Empty; }

            var fileEntity = new FileResourceEntity
            {
                Id = file.Id,
                Title = file.Title,
                ResourceUrl = file.ResourceUrl,
            };

            chapter.Files.Add(fileEntity);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> RemoveFile(Guid chapterId, Guid fileId)
        {
            var chapter = await _context.Chapter.Include(c => c.Files).FirstOrDefaultAsync(c => c.Id == chapterId);
            if (chapter == null) { return Guid.Empty; }

            var fileToRemove = chapter.Files.FirstOrDefault(f => f.Id == fileId);
            if (fileToRemove == null) { return Guid.Empty; }

            chapter.Files.Remove(fileToRemove);

            await _context.SaveChangesAsync();

            return fileId;
        }
    }
}
