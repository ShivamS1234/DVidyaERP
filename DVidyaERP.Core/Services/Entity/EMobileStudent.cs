using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    
    public class EMobileStudent
    {
        /// <summary>
        /// get or set Student Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// get or set Student Name.
        /// </summary>
        public string StudentName { get; set; }

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
        /// get or set Mobile No.
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// get or set Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// get or set Entry Date.
        /// </summary>
        public DateTime EntryDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(EntryDateDateString))
                    return DateTime.MinValue;
                else
                    return DateTime.ParseExact(EntryDateDateString,
                                               Propertise.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            set
            {
                EntryDateDateString = value.ToString(Propertise.DateTimeFormat);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>

        public string EntryDateDateString { get; set; }

        /// <summary>
        /// get or set CreatedBy.
        /// </summary>
        public int CreatedBy { get; set; }

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

        /// <summary>
        /// Gets or sets the UpDated By.
        /// </summary>
        
        public int UpDatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOn Date.
        /// </summary>
        
        public DateTime UpdatedOnDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UpdatedOnDateString))
                    return DateTime.MinValue;
                else
                    return DateTime.ParseExact(UpdatedOnDateString,
                                               Propertise.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            set
            {
                UpdatedOnDateString = value.ToString(Propertise.DateTimeFormat);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        
        public string UpdatedOnDateString { get; set; }

        /// <summary>
        /// get or set Parent Name.
        /// </summary>
        
        public string ParentName { get; set; }

        /// <summary>
        /// get or set ParentCode.
        /// </summary>
        
        public int ParentCode { get; set; }

        /// <summary>
        /// get or set Class Name.
        /// </summary>
        
        public string ClassName { get; set; }

        /// <summary>
        /// get or set Class Code.
        /// </summary>
        
        public int ClassCode { get; set; }

        /// <summary>
        /// get or set Stream Name.
        /// </summary>
        
        public string StreamName { get; set; }

        /// <summary>
        /// get or set StreamCode.
        /// </summary>
        
        public int StreamCode { get; set; }

        /// <summary>
        /// get or set sectionn Name.
        /// </summary>
        
        public string SectionName { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        
        public int SectionCode { get; set; }

        public byte[] Image { get; set; }
    }
}
