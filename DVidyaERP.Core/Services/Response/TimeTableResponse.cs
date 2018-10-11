using DVidyaERP.Core.Services.Entity;
using System.Collections.Generic;

namespace DVidyaERP.Core.Services.Response
{
    public class TimeTableResponse
    {
        public IList<EMobileTimeTable> TimeTable { get; set; }
    }
}
