using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Entities
{
    public class FileResourceEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ResourceUrl { get; set; } = string.Empty;
    }
}
