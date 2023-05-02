using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    //взаємодія з базою даних для зберігання, отримання, оновлення та видалення даних про оголошення
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //повертає колекцію всіх оголошень, що зберігаються в базі даних
        public IEnumerable<Announcement> GetAllAnnouncements()
        {
            return _unitOfWork.AnnouncementRepository.GetAllAsync().Result;
        }
        // повертає оголошення з вказаним ідентифікатором
        public Announcement GetAnnouncementById(int id)
        {
            return _unitOfWork.AnnouncementRepository.GetByIdAsync(id).Result;
        }
        //додає оголошення до бази даних та зберігає зміни
        public void AddAnnouncement(Announcement announcement)
        {
            _unitOfWork.AnnouncementRepository.AddAsync(announcement).Wait();
            _unitOfWork.SaveChanges();
        }
        //оновлює оголошення у базі даних та зберігає зміни
        public void UpdateAnnouncement(Announcement announcement)
        {
            _unitOfWork.AnnouncementRepository.Update(announcement);
            _unitOfWork.SaveChanges();
        }
        //видаляє оголошення з вказаним ідентифікатором з бази даних та зберігає зміни
        public void DeleteAnnouncement(int id)
        {
            _unitOfWork.AnnouncementRepository.DeleteAsync(id).Wait();
            _unitOfWork.SaveChanges();
        }
    }
}
