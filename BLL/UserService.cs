using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //взаємодія з базою даних для зберігання, отримання, оновлення та видалення даних користувачів
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //повертає колекцію всіх користувачів, що зберігаються в базі даних
        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.GetAllAsync().Result;
        }
        // повертає користувача з вказаним ідентифікатором
        public User GetUserById(int id)
        {
            return _unitOfWork.UserRepository.GetByIdAsync(id).Result;
        }
        //додає нового користувача до бази даних та зберігає зміни
        public void AddUser(User user)
        {
            _unitOfWork.UserRepository.AddAsync(user).Wait();
            _unitOfWork.SaveChanges();
        }
        //оновлює існуючого користувача у базі даних та зберігає зміни
        public void UpdateUser(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }
        //видаляє користувача з вказаним ідентифікатором з бази даних та зберігає зміни
        public void DeleteUser(int id)
        {
            _unitOfWork.UserRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
