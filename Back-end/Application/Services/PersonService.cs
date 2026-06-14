using Archive_DbContext.Entities;
using Archive_DbContext.Repositories;
using Domain.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository personRepository)
        {
            _repository = personRepository;
        }

        public async Task<List<Person>> GetAllPerson() // БЕЗ CHAPTERS!!!
        {
            return await _repository.GetAll();
        }

        public async Task<Person?> GetPersonById(Guid id)
        {
            return await _repository.GetPersonById(id);
        }

        public async Task<Guid> CreateNewPerson(Person person)
        {
            return await _repository.CreateNewPerson(person);
        }

        public async Task<Guid> UpdatePersonInfo(Guid id, string firstName, string lastName, string surname, string biography, DateOnly? birthday, DateOnly? death)
        {
            return await _repository.UpdatePersonInfo(id, firstName, lastName, surname, biography, birthday, death);
        }

        public async Task<Guid> UpdateFatherId(Guid id, Guid father)
        {
            return await _repository.UpdateFatherId(id, father);
        }

        public async Task<Guid> UpdateMotherId(Guid id, Guid mother)
        {
            return await _repository.UpdateMotherId(id, mother);
        }

        public async Task<Guid> AddChapter(Guid personId, Guid chapterId)
        {
            return await _repository.AddChapter(personId, chapterId);
        }


        public async Task<Guid> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
