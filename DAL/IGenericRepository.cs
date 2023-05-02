using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // Загальний інтерфейс репозиторію з методами для доступу до таблиць бази даних
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // Асинхронний метод для отримання всіх записів таблиці TEntity з бази даних
        Task<IEnumerable<TEntity>> GetAllAsync();
        // Асинхронний метод для отримання запису таблиці TEntity з бази даних за вказаним ідентифікатором
        Task<TEntity> GetByIdAsync(int id);

        // Асинхронний метод для додавання запису таблиці TEntity до бази даних
        Task<TEntity> AddAsync(TEntity entity);

        // Синхронний метод для оновлення запису таблиці TEntity в базі даних
        TEntity Update(TEntity entity);
        // Асинхронний метод для видалення запису таблиці TEntity з бази даних за вказаним ідентифікатором
        Task<bool> DeleteAsync(int id);
    }
}
