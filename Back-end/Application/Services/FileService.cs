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
    public class FileService : IFileService
    {
        private readonly IFileRepository _repository;
        public FileService(IFileRepository fileRepository)
        {
            _repository = fileRepository;
        }

        public async Task<List<FileResource>> GetAllFiles()
        {
            return await _repository.GetAll();
        }

        public async Task<FileResource?> GetFileById(Guid id)
        {
            return await _repository.GetFileById(id);
        }

        public async Task<Guid> CreateNewFileResource(FileResource file)
        {
            return await _repository.CreateNewFile(file);
        }

        public async Task<Guid> UpdateFile(Guid id, string title, string url)
        {
            return await _repository.Update(id, title, url);
        }

        public async Task<Guid> DeleteFile(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
