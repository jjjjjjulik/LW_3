using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceLocator
    {
        private readonly Func<IUnitOfWork> _unitOfWorkFactory;

        public ServiceLocator(Func<IUnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IAnnouncementService CreateAnnouncementService()
        {
            return new AnnouncementService(_unitOfWorkFactory());
        }

        public ICategoryService CreateCategoryService()
        {
            return new CategoryService(_unitOfWorkFactory());
        }

        public ITagService CreateTagService()
        {
            return new TagService(_unitOfWorkFactory());
        }

        public IUserService CreateUserService()
        {
            return new UserService(_unitOfWorkFactory());
        }
    }
}
