using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    
    public class FacultyTransactionTable
    {
        [PrimaryKey, NotNull,AutoIncrement]
        public int Code { get; set; }
        public int FacultyMaster_Code { get; set; }
        public int ClassMaster_Code { get; set; }
        public int SectionMaster_Code { get; set; }
        public int SubjectMaster_Code { get; set; }
        public bool IsClassIncharge { get; set; }
    }
    //Employ Table
}


