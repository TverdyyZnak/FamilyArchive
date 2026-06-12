using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Entities
{
    public class PersonEntity
    {
        public Guid Id { get; set; }
        public int TreeLVL { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string ShortBiography { get; set; } = string.Empty;
        public DateOnly? Birthday { get; set; } = null;
        public DateOnly? DayOfDeath { get; set; } = null;
        public Guid? FatherId { get; set; } = null;
        public Guid? MotherId { get; set; } = null;
        public List<ChapterEntity> Chapters { get; set; } = new List<ChapterEntity>();
    }
}
