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
        public CategoryRepository(MyProjectDbContext context) : base(context)
        {
        }

        // Тут можна додати специфічні методи для CategoryRepository
    }
}
