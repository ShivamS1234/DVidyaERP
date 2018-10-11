
using System.Collections.Generic;

namespace DVidyaERP.Core.Services.Entity
{
    public class ClassAttendanceRequest
    {
        public long UserID { get; set; }

        public int UserType { get; set; }

        public long FacultyID { get; set; }

        public long ClassID { get; set; }

        public string DateString { get; set; }

        public IList<EClassAttendance> ClassAttendance { get; set; }
        
    }
}
