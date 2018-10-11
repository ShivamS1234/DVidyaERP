using System;
using SQLite.Net.Attributes;

namespace DVidyaERP.Core.Models.Tables
{
    public class TimeTableMaster
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int ID { get; set; }
        /// <summary>
        /// get or set TimeTable Master Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// get or set Class Code.
        /// </summary>
        public int ClassMaster_Code { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        public int StreamMaster_Code { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        public int SectionMaster_Code { get; set; }
        [MaxLength(50)]
        /// <summary>
        /// get or set Entry Date.
        /// </summary>
        public string EntryDate { get; set; }

        public Boolean IsSent { get; set; }
    }
}
