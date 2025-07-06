using System.Collections.Generic;
using KhanhTin.BusinessLogic.DTOs;

namespace KhanhTin.BusinessLogic.Interfaces
{
    public interface ITagService
    {
        IEnumerable<TagDto> GetAllTags();
        TagDto GetTagById(int id);
        void CreateTag(TagCreateDto dto);
        void UpdateTag(TagUpdateDto dto);
        void DeleteTag(int id);
        bool CanDeleteTag(int id);
    }
}
