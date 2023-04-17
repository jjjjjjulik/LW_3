using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.GetAllAsync().Result;
        }

        public User GetUserById(int id)
        {
            return _unitOfWork.UserRepository.GetByIdAsync(id).Result;
        }

        public void AddUser(User user)
        {
            _unitOfWork.UserRepository.AddAsync(user).Wait();
            _unitOfWork.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _unitOfWork.UserRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
