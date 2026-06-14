using Archive_DbContext.Entities;
using Domain.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Person>> GetAll() //Неполная информация
        {
            var personEntity = await _context.Person.Include(p => p.Chapters).ToListAsync();
            var persons = personEntity.Select(p =>
            {
                var person = Person.Create(p.Id, p.FirstName, p.LastName, p.Surname, p.ShortBiography, p.Birthday, p.DayOfDeath, p.FatherId, p.MotherId).person;
                person.Chapters = p.Chapters.Select(c => Chapter.Create(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate).chapter).ToList();
                return person;
            }).ToList();

            return persons;
        }

        public async Task<Person?> GetPersonById(Guid id)
        {
            var p = await _context.Person.Include(p => p.Chapters).ThenInclude(c => c.Files).FirstOrDefaultAsync(p => p.Id == id);
            if (p == null) { return null; }
            var person = Person.Create(p.Id, p.FirstName, p.LastName, p.Surname, p.ShortBiography, p.Birthday, p.DayOfDeath, p.FatherId, p.MotherId).person;

            person.Chapters = p.Chapters.Select(c =>
            {
                var chapter = Chapter.Create(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate).chapter;
                chapter.Files = c.Files.Select(f => FileResource.Create(f.Id, f.Title, f.ResourceUrl).file).ToList();
                return chapter;
            }).ToList();

            return person;
        }

        public async Task<Guid> CreateNewPerson(Person person)
        {
            PersonEntity entity = new PersonEntity
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Surname = person.Surname,
                ShortBiography = person.ShortBiography,
                Birthday = person.Birthday,
                DayOfDeath = person.DayOfDeath,
                FatherId = person.FatherId,
                MotherId = person.MotherId,
            };

            await _context.Person.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> UpdatePersonInfo(Guid id, string firstName, string lastName, string surname, string biography, DateOnly? birthday, DateOnly? death)
        {
            int rowCount = await _context.Person.Where(p => p.Id == id).ExecuteUpdateAsync(o => o
            .SetProperty(p => p.FirstName, p => firstName)
            .SetProperty(p => p.LastName, p => lastName)
            .SetProperty(p => p.Surname, p => surname)
            .SetProperty(p => p.ShortBiography, p => biography)
            .SetProperty(p => p.Birthday, p => birthday)
            .SetProperty(p => p.DayOfDeath, p => death)
            );

            if (rowCount == 0) { return Guid.Empty; }
            return id;
        }

        public async Task<Guid> UpdateFatherId(Guid id, Guid father)
        {
            int rowCount = await _context.Person.Where(p => p.Id == id).ExecuteUpdateAsync(o => o
            .SetProperty(p => p.FatherId, p => father)
            );

            if (rowCount == 0) { return Guid.Empty; }
            return id;
        }

        public async Task<Guid> UpdateMotherId(Guid id, Guid mother)
        {
            int rowCount = await _context.Person.Where(p => p.Id == id).ExecuteUpdateAsync(o => o
            .SetProperty(p => p.MotherId, p => mother)
            );

            if (rowCount == 0) { return Guid.Empty; }
            return id;
        }

        public async Task<Guid> AddChapter(Guid personId, Guid chapterId)
        {
            var person = await _context.Person.Include(p => p.Chapters).FirstOrDefaultAsync(p => p.Id == personId);
            if (person == null) { return Guid.Empty; }

            var chapter = await _context.Chapter.FirstOrDefaultAsync(c => c.Id == chapterId);
            if (chapter == null) { return Guid.Empty; }

            if (!person.Chapters.Contains(chapter))
            {
                person.Chapters.Add(chapter);
                await _context.SaveChangesAsync();
                return person.Id;
            }
            else
            {
                return person.Id;
            }
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Person.Where(p => p.Id == id).ExecuteDeleteAsync();
            return id;
        }
    }
}
