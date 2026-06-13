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
    public class PersonRepository
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
    }
}
