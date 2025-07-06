using System.ComponentModel.DataAnnotations;

namespace KhanhTin.BusinessLogic.DTOs
{
    public class TagDto
    {
        public int TagID { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
        public string TagName { get; set; }

        [StringLength(200, ErrorMessage = "Note cannot exceed 200 characters")]
        public string Note { get; set; }

        public int NewsCount { get; set; }
        public bool CanDelete => NewsCount == 0;
    }

    public class TagCreateDto
    {
        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
        public string TagName { get; set; }

        [StringLength(200, ErrorMessage = "Note cannot exceed 200 characters")]
        public string Note { get; set; }
    }

    public class TagUpdateDto
    {
        public int TagID { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
        public string TagName { get; set; }

        [StringLength(200, ErrorMessage = "Note cannot exceed 200 characters")]
        public string Note { get; set; }
    }

    public class TagListDto
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public string Note { get; set; }
        public int NewsCount { get; set; }

        public bool CanDelete => NewsCount == 0;
    }
}
