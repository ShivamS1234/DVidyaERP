using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class FacultyMasterTable
    {
        [PrimaryKey, NotNull,AutoIncrement]
        public int ID { get; set; }
        public int Code { get; set; }
        [MaxLength(50)]
        public string FacultyName { get; set; }
        public string FacultyPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImageName { get; set; }
        public string Gender { get; set; }
        public DateTime EntryDate { get; set; }
        [MaxLength(50)]
        public int MasterId { get; set; }
        public string EmailAddress { get; set; }
        public string JoiningDate { get; set; }
        public int UserType { get; set; }
        public Boolean Status { get; set; }
        public Boolean IsClassIncharge { get; set; }
        [MaxLength(600)]
        public string PermanentAddress { get; set; }
        [MaxLength(10)]
        public string FinYear { get; set; }
        public string PhoneNo { get; set; }
        [MaxLength(800)]
        public string Qualification { get; set; }
    }
    //Employ Table
}


