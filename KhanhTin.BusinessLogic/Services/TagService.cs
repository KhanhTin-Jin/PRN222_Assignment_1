using System.Collections.Generic;
using System.Linq;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.BusinessLogic.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<TagDto> GetAllTags()
        {
            return _tagRepository.GetAll().Select(MapToDto);
        }

        public TagDto GetTagById(int id)
        {
            var tag = _tagRepository.GetById(id);
            return tag == null ? null : MapToDto(tag);
        }

        public void CreateTag(TagCreateDto dto)
        {
            var tag = new Tag
            {
                TagName = dto.TagName,
                Note = dto.Note
            };

            _tagRepository.Add(tag);
            _tagRepository.SaveChanges();
        }

        public void UpdateTag(TagUpdateDto dto)
        {
            var tag = _tagRepository.GetById(dto.TagID);
            if (tag == null)
            {
                return;
            }

            tag.TagName = dto.TagName;
            tag.Note = dto.Note;

            _tagRepository.Update(tag);
            _tagRepository.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            _tagRepository.Delete(id);
            _tagRepository.SaveChanges();
        }

        public bool CanDeleteTag(int id)
        {
            return !_tagRepository.HasNews(id);
        }

        private TagDto MapToDto(Tag tag)
        {
            return new TagDto
            {
                TagID = tag.TagID,
                TagName = tag.TagName,
                Note = tag.Note,
                NewsCount = _tagRepository.CountNewsWithTag(tag.TagID)
            };
        }
    }
}
