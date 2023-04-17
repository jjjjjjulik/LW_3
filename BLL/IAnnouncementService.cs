using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BLL
{
    public interface IAnnouncementService
    {
        IEnumerable<Announcement> GetAllAnnouncements();
        Announcement GetAnnouncementById(int id);
        void AddAnnouncement(Announcement announcement);
        void UpdateAnnouncement(Announcement announcement);
        void DeleteAnnouncement(int id);
    }
}
