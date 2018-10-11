using System.Collections.Generic;

namespace DVidyaERP.Core.Services.Entity
{
    public class EClassAttendance 
    {
        #region Constructors
        public EClassAttendance()
        {
        }
        #endregion

        #region Data Members

        #endregion

        #region Properties

        /// <summary>
        /// get or set Attendence Master Code.
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

        /// <summary>
        /// get or set Faculty Code.
        /// </summary>
        public int FacultyMaster_Code { get; set; }

        /// <summary>
        /// get or set Subject Code.
        /// </summary>
        public int SubjectMaster_Code { get; set; }

        /// <summary>
        /// Gets or sets the Created Date.
        /// </summary>
      //  public DateTime CreatedDate
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        public string CreatedDateString { get; set; }

        /// <summary>
        /// Gets or sets the Attendence Date.
        /// </summary>
        //public DateTime AttendenceDate
        //{
        //    get;set;
        //}

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        public string AttendanceDateString { get; set; }

        /// <summary>
        /// get or set Join Date.
        /// </summary>
        //public DateTime UpdateDate
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        public string UpdateDateString { get; set; }

        /// <summary>
        /// get or set the Attendence Transaction.
        /// </summary>
        public IList<EAttendanceTransaction> AttendanceTransaction { get; set; }

        #endregion
    }
}
