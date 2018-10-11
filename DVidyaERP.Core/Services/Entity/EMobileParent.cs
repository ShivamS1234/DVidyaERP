using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EMobileParent
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
        public string GaurdianName { get; set; }

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
        /// get or set Join Date.
        /// </summary>
        public DateTime EntryDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(EntryDateString))
                    return DateTime.MinValue;
                else
                    return DateTime.ParseExact(EntryDateString,
                                               Propertise.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            set
            {
                EntryDateString = value.ToString(Propertise.DateTimeFormat);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        public string EntryDateString { get; set; }

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
