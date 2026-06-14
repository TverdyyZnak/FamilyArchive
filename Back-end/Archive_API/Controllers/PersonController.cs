using Application.Services;
using Archive_API.Contracts.ChapterContracts;
using Archive_API.Contracts.FileContracts;
using Archive_API.Contracts.PersonContracts;
using Domain.Classes;
using Microsoft.AspNetCore.Mvc;

namespace Archive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;
        public PersonController(IPersonService personService)
        {
            _service = personService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonShortResponse>>> GetAllPersons()
        {
            var persons = await _service.GetAllPerson();
            var response = persons.Select(p => new PersonShortResponse(p.Id, p.FirstName, p.LastName, p.Surname, p.ShortBiography, p.Birthday, p.DayOfDeath)).ToList();
            return Ok(response);
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<PersonFullResponse>> GetPersonById(Guid id)
        {
            var person = await _service.GetPersonById(id);
            if (person == null) { return BadRequest("Человека с таким id не существует"); }
            
            List<ChapterResponse> chapters = person.Chapters.Select(c =>
            {
                var files = c.Files.Select(f => new FileResponse(f.Id, f.Title, f.ResourceUrl)).ToList();
                var chapter = new ChapterResponse(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate, files);
                return chapter;
            }).ToList();
            var response = new PersonFullResponse(person.Id, person.FirstName, person.LastName, person.Surname, person.ShortBiography, person.Birthday, person.DayOfDeath, chapters);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdatePerson(Guid id, [FromBody] PersonRequest request )
        {
            Guid resp = await _service.UpdatePersonInfo(id, request.firstName, request.lastName, request.surname, request.biography, request.birthday, request.death);
            if (resp == Guid.Empty) { return BadRequest("Пользователя с данным id не существует"); }
            return Ok(resp);
        }

        [HttpPut("father-id")]
        public async Task<ActionResult<Guid>> UpdateFatherId(Guid personId, Guid fatherId)
        {
            Guid resp = await _service.UpdateFatherId(personId, fatherId);
            if (resp == Guid.Empty) { return BadRequest("Пользователя с указанным id не существует"); }
            return Ok(resp);
        }

        [HttpPut("mother-id")]
        public async Task<ActionResult<Guid>> UpdateMotherId(Guid personId, Guid motherId)
        {
            Guid resp = await _service.UpdateMotherId(personId, motherId);
            if (resp == Guid.Empty) { return BadRequest("Пользователя с указанным id не существует"); }
            return Ok(resp);
        }

        [HttpPut("add-chapter")]
        public async Task<ActionResult<Guid>> AddChapter(Guid personId, Guid chapterId)
        {
            Guid resp = await _service.AddChapter(personId, chapterId);
            if (resp == Guid.Empty) { return BadRequest("Пользователя или главы с данным id не существуе"); }
            return Ok(resp);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNewPerson([FromBody] PersonRequest request)
        {
            var (person, error) = Person.Create(Guid.NewGuid(), request.firstName, request.lastName, request.surname, request.biography, request.birthday, request.death, null, null);
            if(error != string.Empty) { return BadRequest(error); }

            return Ok(await _service.CreateNewPerson(person));
        }
        

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeletePerson(Guid id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}
