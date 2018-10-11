using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class ClassTransactionTable
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int Code { get; set; }
        public int ClassMaster_Code { get; set; }
        public int SubjectMaster_Code { get; set; }
    }
    //Employ Table
}


