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
    }
}
