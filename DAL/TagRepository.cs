using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Клас репозиторію для роботи з сутністю Tag
    public class TagRepository : GenericRepository<Tag>, ITagRepository 
    {
        private readonly MyProjectDbContext _context;

        public TagRepository(MyProjectDbContext context) : base(context)
        {
            _context = context;
        }
        // Методи для роботи з таблицею Tags
        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public void Add(Tag entity)
        {
            _context.Tags.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Tag entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tag = GetById(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
        }
    }
}
