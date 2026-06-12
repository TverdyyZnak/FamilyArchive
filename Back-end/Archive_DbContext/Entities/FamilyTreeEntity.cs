using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Entities
{
    public class FamilyTreeEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid MainUserId { get; set; }
        public List<Guid> usersId { get; set; } = new List<Guid>();
        public List<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
    }
}
