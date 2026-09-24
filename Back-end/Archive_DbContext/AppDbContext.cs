using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archive_DbContext.Entities;
using Domain.Classes;
using Microsoft.EntityFrameworkCore;

namespace Archive_DbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FamilyTreeEntity>()
                .HasOne<UserEntity>()
                .WithMany(u => u.OwnedTrees)
                .HasForeignKey(t => t.MainUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FamilyTreeEntity>()
                .HasMany(t => t.Users)
                .WithMany(u => u.FamilyTrees)
                .UsingEntity(j => j.ToTable("FamilyTreeUsers"));

            modelBuilder.Entity<PersonEntity>()
                .HasOne(p => p.FamilyTree)
                .WithMany(t => t.Persons)
                .HasForeignKey(p => p.ArchiveId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChapterEntity>()
                .HasOne(c => c.Person)
                .WithMany(p => p.Chapters)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<UserEntity> User { get; set; }
        public DbSet<PersonEntity> Person { get; set; }
        public DbSet<FileResourceEntity> File { get; set; }
        public DbSet<FamilyTreeEntity> FamilyTree { get; set; }
        public DbSet<ChapterEntity> Chapter { get; set; }
    }
}
