using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Інтерфейс репозиторію для роботи з сутністю Tag
    public interface ITagRepository: IGenericRepository<Tag>
    {
        // Метод для отримання всіх записів таблиці Tags з бази даних
        IEnumerable<Tag> GetAll();
        // Метод для отримання запису таблиці Tags з бази даних за вказаним ідентифікаторо
        Tag GetById(int id);
        // Метод для додавання запису таблиці Tags до бази даних
        void Add(Tag tag);
        // Метод для оновлення запису таблиці Tags в базі даних
        void Update(Tag tag);
        // Метод для видалення запису таблиці Tags з бази даних за вказаним ідентифікатором
        void Delete(int id);
    }
}
