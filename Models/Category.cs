using System;
using System.Collections.Generic;


namespace Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
