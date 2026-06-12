using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archive_DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Archive_DbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<PersonEntity> Person { get; set; }
        public DbSet<FileResourceEntity> File { get; set; }
        public DbSet<FamilyTreeEntity> FamilyTree { get; set; }
        public DbSet<ChapterEntity> Chapter { get; set; }
    }
}
