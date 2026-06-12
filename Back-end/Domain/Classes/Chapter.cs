using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class Chapter : IEntity
    {
        public Guid Id { get; }
        public int SerialNumber { get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public DateOnly? StartDate { get; } = null;
        public DateOnly? EndDate { get; } = null;
        public List<FileResource> Files { get; set; } = new List<FileResource>();
            

        private Chapter(Guid id, int serial, string title, string description, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            Id = id;
            SerialNumber = serial;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
        public static (Chapter chapter, string error) Create(Guid id, int serial, string title, string description, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            string errorString = string.Empty;

            if(title.Length > Params._maxNameLength)
            {
                errorString += $"Название главы привышает лимит в {Params._maxNameLength} символов; ";
            }

            if(description.Length > Params._maxFullDescriptionLength)
            {
                errorString += $"Описание главы привышает лимит в {Params._maxFullDescriptionLength} символов; ";
            }

            return (new Chapter(id, serial, title, description, startDate, endDate), errorString);
        }
    }
}
