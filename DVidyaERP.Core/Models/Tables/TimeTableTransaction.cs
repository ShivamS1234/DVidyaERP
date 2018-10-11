using SQLite.Net.Attributes;
namespace DVidyaERP.Core.Models.Tables
{
    public class TimeTableTransaction
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int ID { get; set; }
        /// <summary>
        /// get or set TimeTable Transaction RowNo.
        /// </summary>
        public int RowNo { get; set; }

        /// <summary>
        /// get or set TimeTable Transaction Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// get or set TimeTable Master Code.
        /// </summary>
        public int TimeTableMaster_Code { get; set; }
        [MaxLength(200)]
        public string DayofWeek { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture1 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture2 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture3 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture4 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture5 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture6 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture7 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture8 { get; set; }

        /// <summary>
        /// get or set TimeTable Lecture No.
        /// </summary>
        public int Lecture9 { get; set; }
    }
}
