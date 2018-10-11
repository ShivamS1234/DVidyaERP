using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EMobileSection
    {
        /// <summary>
        /// get or set Section Code.
        /// </summary>
        public int Code { get; set; } 

        /// <summary>
        ///  get or set Section Name.
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// get or set Sort Order.
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
