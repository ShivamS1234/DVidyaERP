using System;
using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    //LogInTable
    public class LogInTable
    {
        [PrimaryKey,NotNull]
        public int ID { get; set; }
        public int Code { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public DateTime EntryDate { get; set; }
        [MaxLength(50)]
        public int MasterId { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public Boolean Status { get; set; }
        [MaxLength(80)]
        public string DeviceId { get; set; }
        public bool DeviceActivated { get; set; }
        public DateTime UpdateDate { get; set; }
        public Boolean Configured { get; set; }
        public Boolean AdminUser { get; set; }
        public DateTime DCRequestDateString { get; set; }
        public Boolean DCRequested { get; set; }
        public Boolean DeviceOnline { get; set; }
        /// <summary>
        /// get or set Faculty Name.
        /// </summary>
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        [MaxLength(100)]
        public string Address1 { get; set; }
    }
    //Employ Table
}

