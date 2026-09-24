using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<FamilyTreeEntity> FamilyTrees { get; set; } = new List<FamilyTreeEntity>();
        public List<FamilyTreeEntity> OwnedTrees { get; set;} = new List<FamilyTreeEntity>();
    }
}
