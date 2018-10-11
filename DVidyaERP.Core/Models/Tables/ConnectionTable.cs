using System;
//using SQLite;
using SQLite.Net.Attributes;

namespace DVidyaERP.Core.Models.Tables
{
        //LogInTable
        public class ConnectionTable
        {
        [PrimaryKey,AutoIncrement,NotNull]
        public int Id { get; set; }
        [MaxLength(15)]
        public string ServerName { get; set; }
        [MaxLength(5)]
        public string PortNo { get; set; }
        }
        //Employ Table
}

