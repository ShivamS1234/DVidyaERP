
namespace DVidyaERP.Core.Services.Entity
{
    public class EAttendanceTransaction
    {
        #region Constructors

        public EAttendanceTransaction()
        {
        }

        #endregion

        #region Data Members

        #endregion

        #region Properties

        /// <summary>
        /// get or set Attendence Transaction Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// get or set Class Code.
        /// </summary>
        public int AttendanceMaster_Code { get; set; }

        /// <summary>
        /// get or set Status.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// get or set StudentName.
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// get or set StudentCode.
        /// </summary>
        public int StudentMaster_Code { get; set; }

        /// <summary>
        /// get or set Faculty Code.
        /// </summary>
        public int FacultyMaster_Code { get; set; }

        #endregion
    }
}
