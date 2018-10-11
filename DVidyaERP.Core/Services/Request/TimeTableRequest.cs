
namespace DVidyaERP.Core.Services.Request
{
    public class TimeTableRequest
    {
        public long UserID { get; set; }

        public int UserType { get; set; }

        public string DateString { get; set; }

        public bool CheckStatus { get; set; }
    }
}
