
using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EMobileFaculty
    {
        /// <summary>
        /// get or set Faculty Code
        /// </summary>
        public int Code { get; set; } 

        /// <summary>
        /// get or set Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// get or set Faculty Name.
        /// </summary>
        public string FacultyName { get; set; }

        /// <summary>
        /// get or set Faculty Name.
        /// </summary>
        public byte[] FacultyImage { get; set; }

        /// <summary>
        /// get or set IsClassIncharge.
        /// </summary>
        public string IsClassIncharge { get; set; }

        /// <summary>
        /// get or set Mobile No.
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// get or set Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// get or set Confirm Password.
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// get or set Email Address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// get or set Qualification.
        /// </summary>
        public string Qualification { get; set; }

        /// <summary>
        /// get or set Join Date.
        /// </summary>
        public DateTime JoinDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(JoinDateString))
                    return DateTime.MinValue;
                else
                    return DateTime.ParseExact(JoinDateString,
                                               Propertise.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            set
            {
                JoinDateString = value.ToString(Propertise.DateTimeFormat);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        public string JoinDateString { get; set; }

        /// <summary>
        /// Gets or sets the Created Date.
        /// </summary>
        public DateTime CreatedDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CreatedDateString))
                    return DateTime.MinValue;
                else
                    return DateTime.ParseExact(CreatedDateString,
                                               Propertise.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            set
            {
                CreatedDateString = value.ToString(Propertise.DateTimeFormat);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        public string CreatedDateString { get; set; }
    }
}
