using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
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
