using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //взаємодія з базою даних для зберігання, отримання, оновлення та видалення даних про теги
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //повертає колекцію всіх тегів, що зберігаються в базі даних
        public IEnumerable<Tag> GetAllTags()
        {
            return _unitOfWork.TagRepository.GetAllAsync().Result;
        }
        // повертає тег з вказаним ідентифікатором
        public Tag GetTagById(int id)
        {
            return _unitOfWork.TagRepository.GetByIdAsync(id).Result;
        }
        //додає тег до бази даних та зберігає зміни
        public void AddTag(Tag tag)
        {
            _unitOfWork.TagRepository.AddAsync(tag).Wait();
            _unitOfWork.SaveChanges();
        }
        //оновлює тег у базі даних та зберігає зміни
        public void UpdateTag(Tag tag)
        {
            _unitOfWork.TagRepository.Update(tag);
            _unitOfWork.SaveChanges();
        }
        //видаляє тег з вказаним ідентифікатором з бази даних та зберігає зміни
        public void DeleteTag(int id)
        {
            _unitOfWork.TagRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
