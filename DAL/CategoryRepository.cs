using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Клас репозиторію для роботи з сутністю Category
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository 
    {
        // Закрите поле контексту бази даних
        private readonly MyProjectDbContext _context; 
        // Конструктор класу, який приймає контекст бази даних і передає його базовому класу
        public CategoryRepository(MyProjectDbContext context) : base(context) 
        {
            _context = context;
        }
        // Метод для отримання всіх записів таблиці Categories з бази даних
        public IEnumerable<Category> GetAll()  
        {
            return _context.Categories.ToList();
        }
        // Метод для отримання запису таблиці Categories з бази даних за вказаним ідентифікатором
        public Category GetById(int id) 
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        // Метод для оновлення запису таблиці Categories в базі даних
        public void Add(Category entity)  
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }
        // Метод для оновлення запису таблиці Categories в базі даних
        public void Update(Category entity)  
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        // Метод для видалення запису таблиці Categories з бази даних за вказаним ідентифікатором
        public void Delete(int id) 
        {
            var category = GetById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
