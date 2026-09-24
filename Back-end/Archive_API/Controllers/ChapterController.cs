using Application.Services;
using Archive_API.Contracts.ChapterContracts;
using Archive_API.Contracts.FileContracts;
using Domain.Classes;
using Microsoft.AspNetCore.Mvc;

namespace Archive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _service;
        private readonly IPersonService _personService;

        public ChapterController(IChapterService chapterService, IPersonService personService)
        {
            _service = chapterService;
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<ChapterResponse>> GetAllChapters()
        {
            var chapters = await _service.GetAllChapters();
            var resp = chapters.Select(c =>
            {
                var chapter = new ChapterResponse(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate,
                    c.Files.Select(f => new FileResponse(f.Id, f.Title, f.ResourceUrl)).ToList());

                return chapter;
            }).ToList();

            return Ok(resp);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChapterResponse>> GetChapterById(Guid id)
        {
            var c = await _service.GetChapterById(id);
            if (c == null) { return BadRequest("Главы с данным id не существует"); }
            var resp = new ChapterResponse(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate,
                    c.Files.Select(f => new FileResponse(f.Id, f.Title, f.ResourceUrl)).ToList());

            return Ok(resp);
        }

        [HttpPost("new/{personId:guid}")]
        public async Task<ActionResult<Guid>> CreateNewChapter(Guid personId, [FromBody] ChapterRequest request)
        {
            var personEntity = await _personService.GetPersonById(personId);

            var (chapter, error) = Chapter.Create(Guid.NewGuid(), request.serial, request.title, request.description, request.start, request.end);
            if (error != string.Empty) { return BadRequest(error); }

            chapter.Person = personEntity;
            chapter.PersonId = personId;

            Guid newChapterId = await _service.CreateNewChapter(chapter);
            Console.WriteLine(newChapterId.ToString());
            await _personService.AddChapter(personId, newChapterId);

            return Ok();
        }

        [HttpPut("{chapterId:guid}")]
        public async Task<ActionResult<Guid>> UpdateChapter([FromRoute] Guid chapterId, [FromBody] ChapterRequest request)
        {
            Guid resp = await _service.UpdateChapter(chapterId, request.serial, request.title, request.description, request.start, request.end);
            if (resp == Guid.Empty) { return BadRequest("Главы с данным id не существует"); }
            return Ok(resp);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteChapter(Guid id)
        {
            return Ok(await _service.DeleteChapter(id));
        }

    }
}
