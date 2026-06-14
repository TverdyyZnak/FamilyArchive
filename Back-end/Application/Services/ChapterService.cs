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
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _repository;

        public ChapterService(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Chapter>> GetAllChapters()
        {
            return await _repository.GetAll();
        }

        public async Task<Chapter?> GetChapterById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Guid> CreateNewChapter(Chapter chapter)
        {
            return await _repository.CreateNewChapter(chapter);
        }

        public async Task<Guid> UpdateChapter(Guid id, int serial, string title, string description, DateOnly? start, DateOnly? end)
        {
            return await _repository.Update(id, serial, title, description, start, end);
        }

        public async Task<Guid> DeleteChapter(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<Guid> AddFileToChapter(Guid id, FileResource file)
        {
            return await _repository.AddToFile(id, file);
        }

        public async Task<Guid> RemoveFileFromChapter(Guid chapterId, Guid fileId)
        {
            return await _repository.RemoveFile(chapterId, fileId);
        }
    }
}
