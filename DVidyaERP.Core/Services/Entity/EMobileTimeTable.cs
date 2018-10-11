using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EMobileTimeTable 
    {
        /// <summary>
        /// get or set TimeTable Master Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// get or set Class Code.
        /// </summary>
        public int ClassMaster_Code { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        public int StreamMaster_Code { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        public int SectionMaster_Code { get; set; }

        /// <summary>
        /// get or set Entry Date.
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
                    return DateTime.MaxValue;
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
        /// get or set Join Date.
        /// </summary>
        public DateTime UpdateDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UpdateDateString))
                    return DateTime.MaxValue;
                else
                    return DateTime.ParseExact(UpdateDateString,
                                               Propertise.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            set
            {
                UpdateDateString = value.ToString(Propertise.DateTimeFormat);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// Its used during data syncing between mobile and server.
        /// </summary>
        public string UpdateDateString { get; set; }

        public bool IsSent { get; set; }
        /// <summary>
        /// Gets or sets the Table Transactions.
        /// </summary>
        public IList<EMobileTimeTableTransaction> TableTransactions { get; set; }
    }
}
