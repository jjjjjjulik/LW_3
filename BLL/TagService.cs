using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _unitOfWork.TagRepository.GetAllAsync().Result;
        }

        public Tag GetTagById(int id)
        {
            return _unitOfWork.TagRepository.GetByIdAsync(id).Result;
        }

        public void AddTag(Tag tag)
        {
            _unitOfWork.TagRepository.AddAsync(tag).Wait();
            _unitOfWork.SaveChanges();
        }

        public void UpdateTag(Tag tag)
        {
            _unitOfWork.TagRepository.Update(tag);
            _unitOfWork.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            _unitOfWork.TagRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
