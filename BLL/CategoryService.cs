using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.GetAllAsync().Result;
        }

        public Category GetCategoryById(int id)
        {
            return _unitOfWork.CategoryRepository.GetByIdAsync(id).Result;
        }

        public void AddCategory(Category category)
        {
            _unitOfWork.CategoryRepository.AddAsync(category).Wait();
            _unitOfWork.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            _unitOfWork.CategoryRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
