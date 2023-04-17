using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int id);
        void AddTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(int id);
    }
}
