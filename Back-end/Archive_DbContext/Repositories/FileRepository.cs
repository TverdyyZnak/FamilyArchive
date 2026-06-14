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
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FileResource>> GetAll()
        {
            var fileEntities = await _context.File.AsNoTracking().ToListAsync();
            return fileEntities.Select(f => FileResource.Create(f.Id, f.Title, f.ResourceUrl).file).ToList();
        }

        public async Task<FileResource?> GetFileById(Guid id)
        {
            var fileEntity = await _context.File.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
            if (fileEntity == null) { return null; }

            var resp = FileResource.Create(fileEntity.Id, fileEntity.Title, fileEntity.ResourceUrl).file;
            return resp;
        }

        public async Task<Guid> CreateNewFile(FileResource file)
        {
            FileResourceEntity entity = new FileResourceEntity
            {
                Id = file.Id,
                Title = file.Title,
                ResourceUrl = file.ResourceUrl,
            };

            await _context.File.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string url)
        {
            int rowCount = await _context.File.Where(f => f.Id == id).ExecuteUpdateAsync(o => o
            .SetProperty(f => f.Title, f => title)
            .SetProperty(f => f.ResourceUrl, f => url)
            );

            if (rowCount == 0) { return Guid.Empty; }
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.File.Where(f => f.Id == id).ExecuteDeleteAsync();
            return id;
        }
    }
}
