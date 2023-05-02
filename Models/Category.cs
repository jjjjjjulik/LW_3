using System;
using System.Collections.Generic;
using SQLite;

namespace Models
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
