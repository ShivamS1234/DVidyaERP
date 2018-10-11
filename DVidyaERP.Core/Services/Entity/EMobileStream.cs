using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    
    public class EMobileStream
    {
        /// <summary>
        /// get or set Strem Code.
        /// </summary>
        
        public int Code { get; set; } 

        /// <summary>
        /// Gets or sets the Stream Name. 
        /// </summary>
        
        public string StreamName { get; set; }

        /// <summary>
        /// Gets or sets the Sort Order.
        /// </summary>
        
        public int SortOrder { get; set; }

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
