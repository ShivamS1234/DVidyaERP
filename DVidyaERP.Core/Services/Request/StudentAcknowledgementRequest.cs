using DVidyaERP.Core.Services.Entity;
using System.Collections.Generic;

namespace DVidyaERP.Core.Request
{
    public class StudentAcknowledgementRequest
    {
        public long UserID { get; set; }

        public int UserType { get; set; }

        public IList<EAttendanceTransaction> Transactions { get; set; }
    }
}
