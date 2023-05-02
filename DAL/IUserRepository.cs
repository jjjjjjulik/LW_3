using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserRepository: IGenericRepository<User> // Інтерфейс репозиторію для роботи з сутністю User
    {
        // Метод для отримання всіх записів таблиці Users з бази даних
        IEnumerable<User> GetAll();
        // Метод для отримання запису таблиці Users з бази даних за вказаним ідентифікатором
        User GetById(int id);

        // Метод для додавання запису таблиці Users до бази даних
        void Add(User user);

        // Метод для оновлення запису таблиці Users в базі даних
        void Update(User user);
        // Метод для видалення запису таблиці Users з бази даних за вказаним ідентифікатором
        void Delete(int id);
    }
}
