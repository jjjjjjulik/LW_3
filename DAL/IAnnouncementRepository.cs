using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    // Інтерфейс репозиторію для роботи з сутністю Announcement
    public interface IAnnouncementRepository: IGenericRepository<Announcement>
    {
        // Метод для отримання всіх записів таблиці Announcement з бази даних
        IEnumerable<Announcement> GetAll();
        // Метод для отримання запису таблиці Announcement з бази даних за вказаним ідентифікатором
        Announcement GetById(int id);

        // Метод для додавання запису таблиці Announcement до бази даних
        void Add(Announcement announcement);
        // Метод для оновлення запису таблиці Announcement в базі даних
        void Update(Announcement announcement);
        // Метод для видалення запису таблиці Announcement з бази даних за вказаним ідентифікатором
        void Delete(int id);
    }
}
