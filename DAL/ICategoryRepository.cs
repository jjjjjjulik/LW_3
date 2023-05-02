using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Інтерфейс репозиторію для роботи з сутністю Category
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        // Метод для отримання всіх записів таблиці Categories з бази даних
        IEnumerable<Category> GetAll();
        // Метод для отримання запису таблиці Categories з бази даних за вказаним ідентифікатором
        Category GetById(int id);

        // Метод для додавання запису таблиці Categories до бази даних
        void Add(Category category);
        // Метод для оновлення запису таблиці Categories в базі даних
        void Update(Category category);
        // Метод для видалення запису таблиці Categories з бази даних за вказаним ідентифікатором
        void Delete(int id);
    }
}
