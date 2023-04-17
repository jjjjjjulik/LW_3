using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyProjectDbContext _context;

        private IAnnouncementRepository _announcementRepository;
        private ICategoryRepository _categoryRepository;
        private ITagRepository _tagRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(MyProjectDbContext context)
        {
            _context = context;
        }

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

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        private bool _disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
