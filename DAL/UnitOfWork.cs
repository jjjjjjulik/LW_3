using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Клас, який реалізує інтерфейс IUnitOfWork та забезпечує зручний доступ до всіх репозиторіїв
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyProjectDbContext _context;
        // Приватні змінні для збереження екземплярів кожного репозиторію
        private IAnnouncementRepository _announcementRepository;
        private ICategoryRepository _categoryRepository;
        private ITagRepository _tagRepository;
        private IUserRepository _userRepository;
        // Конструктор, який отримує об'єкт контексту бази даних
        public UnitOfWork(MyProjectDbContext context)
        {
            _context = context;
        }
        // Властивості, які повертають екземпляри кожного репозиторію при першому зверненні до властивості
        public IAnnouncementRepository AnnouncementRepository
        {
            get
            {
                if (_announcementRepository == null)
                {
                    _announcementRepository = new AnnouncementRepository(_context);
                }
                return _announcementRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new TagRepository(_context);
                }
                return _tagRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }
        // Метод для збереження всіх змін до бази даних
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        // Змінна, яка вказує, чи було вже звільнено ресурси
        private bool _disposed = false;
        // Метод для звільнення ресурсів
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        // Реалізація інтерфейсу IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
