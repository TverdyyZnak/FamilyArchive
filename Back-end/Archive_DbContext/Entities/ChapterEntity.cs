using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Entities
{
    public class ChapterEntity
    {
        public Guid Id { get; set; }
        public int SerialNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly? StartDate { get; set; } = null;
        public DateOnly? EndDate { get; set; } = null;
        public List<FileResourceEntity> Files { get; set; } = new List<FileResourceEntity>();
    }
}
