using System.Collections.Generic;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Interfaces
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Tag GetById(int id);
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(int id);
        bool HasNews(int tagId);
        int CountNewsWithTag(int tagId);
        void SaveChanges();
    }
}
