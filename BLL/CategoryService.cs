using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //взаємодія з базою даних для зберігання, отримання, оновлення та видалення даних про катгорії
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //повертає колекцію всіх категорій, що зберігаються в базі даних
        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.GetAllAsync().Result;
        }
        // повертає категорію з вказаним ідентифікатором
        public Category GetCategoryById(int id)
        {
            return _unitOfWork.CategoryRepository.GetByIdAsync(id).Result;
        }
        //додає категорію до бази даних та зберігає зміни
        public void AddCategory(Category category)
        {
            _unitOfWork.CategoryRepository.AddAsync(category).Wait();
            _unitOfWork.SaveChanges();
        }
        //оновлює категорію у базі даних та зберігає зміни
        public void UpdateCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.SaveChanges();
        }
        //видаляє категорію з вказаним ідентифікатором з бази даних та зберігає зміни
        public void DeleteCategory(int id)
        {
            _unitOfWork.CategoryRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
