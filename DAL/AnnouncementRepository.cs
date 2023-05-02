using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Клас репозиторію для роботи з сутністю Announcement
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository 
    {
        // Закрите поле контексту бази даних
        private readonly MyProjectDbContext _context;
        // Конструктор класу, який приймає контекст бази даних і передає його базовому класу
        public AnnouncementRepository(MyProjectDbContext context) : base(context)  
        {
            _context = context;
        }
        // Метод для отримання всіх записів таблиці Announcement з бази даних
        public IEnumerable<Announcement> GetAll()    
        {
            return _context.Announcements.ToList();
        }
        // Метод для отримання запису таблиці Announcement з бази даних за вказаним ідентифікатором
        public Announcement GetById(int id) 
        {
            return _context.Announcements.FirstOrDefault(a => a.Id == id);
        }
        // Метод для додавання запису таблиці Announcement до бази даних
        public void Add(Announcement entity)  
        {
            _context.Announcements.Add(entity);
            _context.SaveChanges();
        }
        // Метод для оновлення запису таблиці Announcement в базі даних
        public void Update(Announcement entity) 
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        // Метод для видалення запису таблиці Announcement з бази даних за вказаним ідентифікатором
        public void Delete(int id) 
        {
            var announcement = GetById(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                _context.SaveChanges();
            }
        }
        // Метод для отримання списку записів таблиці Announcement з бази даних за заданою категорією
        public List<Announcement> GetAnnouncementsByCategory(int categoryId) 
        {
            return _context.Announcements.Where(a => a.CategoryId == categoryId).ToList();
        }
        // Метод для отримання списку записів таблиці Announcement з бази даних за вказаним тегом
        public List<Announcement> GetAnnouncementsByTag(int tagId) 
        {
            return _context.AnnouncementTags
                           .Where(at => at.TagId == tagId)
                           .Select(at => at.Announcement)
                           .ToList();
        }
        // Метод для отримання списку записів таблиці Announcement з бази даних за вказаним користувачем
        public List<Announcement> GetAnnouncementsByUser(int userId) 
        {
            return _context.Announcements.Where(a => a.UserId == userId).ToList();
        }
    }

}
