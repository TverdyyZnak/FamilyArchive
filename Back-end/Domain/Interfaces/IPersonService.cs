using Domain.Classes;

namespace Application.Services
{
    public interface IPersonService
    {
        Task<Guid> AddChapter(Guid personId, Guid chapterId);
        Task<Guid> CreateNewPerson(Person person);
        Task<Guid> Delete(Guid id);
        Task<List<Person>> GetAllPerson();
        Task<Person?> GetPersonById(Guid id);
        Task<Guid> UpdateFatherId(Guid id, Guid father);
        Task<Guid> UpdateMotherId(Guid id, Guid mother);
        Task<Guid> UpdatePersonInfo(Guid id, string firstName, string lastName, string surname, string biography, DateOnly? birthday, DateOnly? death);
    }
}