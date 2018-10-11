using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class StreamMasterTable
    {
        [PrimaryKey, NotNull]
        public int Code { get; set; }
        public int SortOrder { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [MaxLength(100)]
        public string StreamName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
    //Employ Table
}


