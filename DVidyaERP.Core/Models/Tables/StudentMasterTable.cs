using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class StudentMasterTable
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int SNo { get; set; }
        public int Code { get; set; }
        public int SortOrder { get; set; }
        public int ParentMaster_Code { get; set; }
        public int ClassMaster_Code { get; set; }
        public int StreamMaster_Code { get; set; }
        public int SectionMaster_Code { get; set; }
        [MaxLength(200)]
        public string StudentName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Device { get; set; }
        public string ImageName { get; set; }
        public bool DeviceActivated { get; set; }
        public bool Status { get; set; }
        [MaxLength(50)]
        public string MobileNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    //Employ Table
}


