using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class MyProjectDbContext : DbContext
    {
        public MyProjectDbContext() : base()
        {
        }
        public MyProjectDbContext(DbContextOptions<MyProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AnnouncementTag> AnnouncementTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John", LastName = "Big", Email = "john@example.com", PasswordHash = "password1" },
                new User { Id = 2, FirstName = "Mary", LastName = "Newman", Email = "mary@example.com", PasswordHash = "password2" },
                new User { Id = 3, FirstName = "Alex", LastName = "Port", Email = "alex@example.com", PasswordHash = "password3" }
            );
            modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Electronics" },
        new Category { Id = 2, Name = "Furniture" },
        new Category { Id = 3, Name = "Clothing" }
    );
            modelBuilder.Entity<Tag>().HasData(
        new Tag { Id = 1, Name = "Tag1" },
        new Tag { Id = 2, Name = "Tag2" },
        new Tag { Id = 3, Name = "Tag3" }
);
            modelBuilder.Entity<User>()
    .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<User>()
    .Property(u => u.Id)
    .IsRequired();

            modelBuilder.Entity<Category>()
    .HasKey(u => u.Id);
            modelBuilder.Entity<Category>()
                .Property(u => u.Name)
                .IsRequired();
            modelBuilder.Entity<Category>()
                .Property(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<Tag>()
.HasKey(u => u.Id);
            modelBuilder.Entity<Tag>()
                .Property(u => u.Name)
                .IsRequired();
            modelBuilder.Entity<Tag>()
                .Property(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<AnnouncementTag>()
                .HasKey(at => new { at.AnnouncementId, at.TagId });

            modelBuilder.Entity<AnnouncementTag>()
                .HasOne(at => at.Announcement)
                .WithMany(a => a.AnnouncementTags)
                .HasForeignKey(at => at.AnnouncementId);

            modelBuilder.Entity<AnnouncementTag>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.AnnouncementTags)
                .HasForeignKey(at => at.TagId);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=myDatabase.db");
        }
    }
}
