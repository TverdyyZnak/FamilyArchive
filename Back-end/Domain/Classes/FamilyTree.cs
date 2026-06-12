using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class FamilyTree : IEntity
    {
        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public Guid MainUserId { get; }
        public List<Guid> usersId { get; set; } = new List<Guid>();
        public List<Person > Persons { get; set; } = new List<Person>();

        private FamilyTree(Guid id, string title, Guid mainUserId)
        {
            Id = id;
            Title = title;
            MainUserId = mainUserId;
            usersId.Add(mainUserId);
        }

        public static (FamilyTree tree, string error) Create(Guid id, string title, Guid mainUserId)
        {
            string errorString = string.Empty;

            if(title.Length > Params._maxNameLength)
            {
                errorString += $"Название привышает лимит в {Params._maxNameLength} символов; ";
            }

            return (new FamilyTree(id, title, mainUserId), errorString);
        }
    }
}
