using System;
using System.Globalization;

namespace DVidyaERP.Core.Global_Method_Propertise
{
    public class Propertise
    {
        public static string DateFormat = "MMM dd, yyyy";

        public static string DateTimeFormat = "MMM dd, yyyy HH:mm:ss";

        public static string DateTimeFormatWithoutSec = "MMM dd, yyyy HH:mm";

        public static string DateTimeFormatForSqlite = "yyyy-MM-dd HH:mm:ss";

        public string ConsoleDateTimeFormat(string datetime, bool formatTypeSQL = true, bool formatTypeSQLITE = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(datetime))
                {
                    if (formatTypeSQL == true)
                    {
                        datetime = Convert.ToDateTime(datetime).ToString(DateTimeFormat);
                    }
                    else if (formatTypeSQLITE == true)
                    {
                        datetime = Convert.ToDateTime(datetime).ToString(DateTimeFormatForSqlite);
                    }
                }
            }
            catch (Exception ex)
            {
                datetime = "";
            }

            return datetime;
        }

        /// <summary>
        /// Get ToDay Date
        /// </summary>
        /// <returns></returns>
        public static string todayDate()
        {
            DateTime now = DateTime.Now.ToLocalTime();
            string todayDate = now.ToString(DateFormat, CultureInfo.InvariantCulture);
            return todayDate;
        }

    }
}
