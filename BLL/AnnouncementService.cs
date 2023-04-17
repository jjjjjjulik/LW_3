using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Announcement> GetAllAnnouncements()
        {
            return _unitOfWork.AnnouncementRepository.GetAllAsync().Result;
        }

        public Announcement GetAnnouncementById(int id)
        {
            return _unitOfWork.AnnouncementRepository.GetByIdAsync(id).Result;
        }

        public void AddAnnouncement(Announcement announcement)
        {
            _unitOfWork.AnnouncementRepository.AddAsync(announcement).Wait();
            _unitOfWork.SaveChanges();
        }

        public void UpdateAnnouncement(Announcement announcement)
        {
            _unitOfWork.AnnouncementRepository.Update(announcement);
            _unitOfWork.SaveChanges();
        }

        public void DeleteAnnouncement(int id)
        {
            _unitOfWork.AnnouncementRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
