using System;
namespace DVidyaERP.Global_Method_Propertise
{
    public static class UserType
    {
        public enum enumUserType
        {
            None = 0,
            Management = 1,
            Faculty = 2,
            Student = 3,
            Parent = 4
        }
        public static enumUserType currentUserType;
        //here we are declare for 
        public static attendanceType currentAttendanceType;
        public enum attendanceType
        {
            Take='A',
            Show='S',
            TimeTable='T'
        }
    }
}
