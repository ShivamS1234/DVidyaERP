using System.Collections.Generic;


namespace DVidyaERP.Core.Services.Request
{
    public class TimeTableAcknowledgementRequest
    {
        public long UserID { get; set; }

        public int UserType { get; set; }

        public int TimeTableMasterCode { get; set; }
    }
}