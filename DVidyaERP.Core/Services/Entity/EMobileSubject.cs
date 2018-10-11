using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    
    public class EMobileSubject
    {
        /// <summary>
        /// gets or sets Subject Code.
        /// </summary>
        
        public int Code { get; set; } 

        /// <summary>
        /// Gets or sets the Subject Name.
        /// </summary>
        
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the Sort Order.
        /// </summary>
        
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the Created By.
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
    }
}
