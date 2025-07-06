using System;

namespace KhanhTin.BusinessLogic.Models
{
    public class AuditInfo
    {
        public int CreatedByID { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedByID { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public bool HasBeenModified => UpdatedByID.HasValue && UpdatedDate.HasValue;
        public string LastModifiedBy => HasBeenModified ? UpdatedByName : CreatedByName;
        public DateTime LastModifiedDate => HasBeenModified ? UpdatedDate.Value : CreatedDate;

        public static AuditInfo Create(int createdById, string createdByName)
        {
            return new AuditInfo
            {
                CreatedByID = createdById,
                CreatedByName = createdByName,
                CreatedDate = DateTime.Now
            };
        }

        public void UpdateAudit(int updatedById, string updatedByName)
        {
            UpdatedByID = updatedById;
            UpdatedByName = updatedByName;
            UpdatedDate = DateTime.Now;
        }
    }
}
