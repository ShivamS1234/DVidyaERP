using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class ClassMasterTable
    {
        [PrimaryKey, NotNull]
        public int Code { get; set; }
        public int BoardMaster_Code { get; set; }
        public int StreamMaster_Code { get; set; }
        public int SectionMaster_Code { get; set; }
        [MaxLength(100)]
        public string ClassName { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    //Employ Table
}


