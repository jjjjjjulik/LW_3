using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Інтерфейс UnitOfWork, який визначає методи для отримання репозиторіїв для кожної сутності і метод для збереження змін в базі даних
    public interface IUnitOfWork : IDisposable
    {
        // Властивість для отримання репозиторію для роботи з сутністю Announcement
        IAnnouncementRepository AnnouncementRepository { get; }
        // Властивість для отримання репозиторію для роботи з сутністю Category
        ICategoryRepository CategoryRepository { get; }
        // Властивість для отримання репозиторію для роботи з сутністю Tag
        ITagRepository TagRepository { get; }
        // Властивість для отримання репозиторію для роботи з сутністю User
        IUserRepository UserRepository { get; }
        // Метод для збереження змін в базі даних
        int SaveChanges();
    }
}
