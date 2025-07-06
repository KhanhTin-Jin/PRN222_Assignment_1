using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KhanhTin.DataAccess.Data;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public TagRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _dbcontext.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _dbcontext.Tags.Find(id);
        }

        public void Add(Tag tag)
        {
            _dbcontext.Tags.Add(tag);
        }

        public void Update(Tag tag)
        {
            _dbcontext.Tags.Update(tag);
        }

        public void Delete(int id)
        {
            var tag = _dbcontext.Tags.Find(id);
            if (tag != null)
            {
                _dbcontext.Tags.Remove(tag);
            }
        }

        public bool HasNews(int tagId)
        {
            return _dbcontext.NewsTags.Any(nt => nt.TagID == tagId);
        }

        public int CountNewsWithTag(int tagId)
        {
            return _dbcontext.NewsTags.Count(nt => nt.TagID == tagId);
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
