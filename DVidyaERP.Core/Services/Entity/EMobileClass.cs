using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EMobileClass
    {

        /// <summary>
        /// get or set Class Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// get or set Class Name.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// get or set Faculty Code.
        /// </summary>
        public int FacultyCode { get; set; }

        /// <summary>
        /// get or set Faculty Name.
        /// </summary>
        public string FacultyName { get; set; }

        /// <summary>
        /// get or set sectionn Name.
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        public int SectionCode { get; set; }

        /// <summary>
        /// get or set sectionn Name.
        /// </summary>
        public string StreamName { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        public int StreamCode { get; set; }

        /// <summary>
        ///  get or set SubjectCode
        /// </summary>
        public int SubjectCode { get; set; }

        /// <summary>
        /// get or set SubjectName.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the Created Date.
        /// </summary>
        public DateTime EntryDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(EntryDateString))
                    return DateTime.MaxValue;
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
        /// Gets or sets the Financial Year.
        /// </summary>
        public string FinYear { get; set; }

    }
}
