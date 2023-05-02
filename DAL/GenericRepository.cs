using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    // Клас репозиторію з загальними методами для доступу до таблиць бази даних
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class 
    {
        // Закрите поле контексту бази даних
        private readonly DbContext _context;
        // Закрите поле DbSet<TEntity>, що використовується для роботи з таблицею сутностей TEntity
        private readonly DbSet<TEntity> _dbSet; 
        // Конструктор класу, який приймає контекст бази даних і встановлює змінні поля
        public GenericRepository(DbContext context) 
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        // Асинхронний метод для отримання всіх записів таблиці TEntity з бази даних
        public async Task<IEnumerable<TEntity>> GetAllAsync() 
        {
            return await _dbSet.ToListAsync();
        }
        // Асинхронний метод для отримання запису таблиці TEntity з бази даних за вказаним ідентифікатором
        public async Task<TEntity> GetByIdAsync(int id) 
        {
            return await _dbSet.FindAsync(id);
        }
        // Асинхронний метод для додавання запису таблиці TEntity до бази даних
        public async Task<TEntity> AddAsync(TEntity entity) 
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        // Синхронний метод для оновлення запису таблиці TEntity в базі даних
        public TEntity Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
        // Асинхронний метод для видалення запису таблиці TEntity з бази даних за вказаним ідентифікатором
        public async Task<bool> DeleteAsync(int id)
        {
            TEntity entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
