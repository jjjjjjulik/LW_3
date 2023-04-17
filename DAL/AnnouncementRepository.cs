using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        private readonly MyProjectDbContext _context;

        public AnnouncementRepository(MyProjectDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Announcement> GetAll()
        {
            return _context.Announcements.ToList();
        }

        public Announcement GetById(int id)
        {
            return _context.Announcements.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Announcement entity)
        {
            _context.Announcements.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Announcement entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var announcement = GetById(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                _context.SaveChanges();
            }
        }

        public List<Announcement> GetAnnouncementsByCategory(int categoryId)
        {
            return _context.Announcements.Where(a => a.CategoryId == categoryId).ToList();
        }

        public List<Announcement> GetAnnouncementsByTag(int tagId)
        {
            return _context.AnnouncementTags
                           .Where(at => at.TagId == tagId)
                           .Select(at => at.Announcement)
                           .ToList();
        }

        public List<Announcement> GetAnnouncementsByUser(int userId)
        {
            return _context.Announcements.Where(a => a.UserId == userId).ToList();
        }
    }

}
