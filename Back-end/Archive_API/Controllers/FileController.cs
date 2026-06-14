using Application.Services;
using Archive_API.Contracts.FileContracts;
using Domain.Classes;
using Microsoft.AspNetCore.Mvc;

namespace Archive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _service;

        public FileController(IFileService fileService)
        {
            _service = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<FileResponse>> GetAllFiles()
        {
            return Ok(await _service.GetAllFiles());
        }
        [HttpGet("by-id")]
        public async Task<ActionResult<FileResponse>> GetFileById(Guid id)
        {
            var file = await _service.GetFileById(id); 
            if(file == null) { return BadRequest("Файла с данным id не найден"); }
            return Ok(file);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNewFile([FromBody] FileRequest request)
        {
            var (file, error) = FileResource.Create(Guid.NewGuid(), request.title, request.url);
            if(error != string.Empty) { return BadRequest(error); }
            return Ok(await _service.CreateNewFileResource(file));
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateFile(Guid id, [FromBody] FileRequest request)
        {
            var resp = await _service.UpdateFile(id, request.title, request.url);
            if (resp == Guid.Empty) { return BadRequest("Файла с таким id не существует"); }
            return Ok(resp);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteFile(Guid id)
        {
            return Ok(await _service.DeleteFile(id));
        }
    }
}
