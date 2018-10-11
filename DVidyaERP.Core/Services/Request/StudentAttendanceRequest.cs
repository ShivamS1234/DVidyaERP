namespace DVidyaERP.Core.Services.Request
{
    public class StudentAttendanceRequest
    {
        public long UserID { get; set; }
        
        public int UserType { get; set; }

        public string DateString { get; set; }
    }
}
