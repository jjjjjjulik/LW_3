using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly MyProjectDbContext _context;

        public CategoryRepository(MyProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

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
