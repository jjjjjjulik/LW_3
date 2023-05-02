using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    // Клас AnnouncementTag представляє сутність зв'язку багато-до-багатьох між об'єктами Announcement та Tag
    public class AnnouncementTag
    {
        public int AnnouncementId { get; set; }
        [ForeignKey("AnnouncementId")]
        public Announcement Announcement { get; set; }

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
