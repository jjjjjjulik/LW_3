using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public interface IAnnouncementRepository
    {
        IEnumerable<Announcement> GetAll();
        Announcement GetById(int id);
        void Add(Announcement announcement);
        void Update(Announcement announcement);
        void Delete(int id);
    }
}
