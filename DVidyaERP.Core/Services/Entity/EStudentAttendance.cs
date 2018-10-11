using System;
using System.Collections.Generic;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EStudentAttendance
    {
        /// <summary>
        /// get or set Student Attendance List.
        /// </summary>
        public IList<EClassAttendance> StudentAttendance { get; set; }
    }
}
