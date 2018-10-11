using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EStudentDetails : EBaseCode
    {
        /// <summary>
        /// get or sets Student data
        /// </summary>
        public EMobileStudent Student { get; set; }
        /// <summary>
        /// Gets or sets the Student Classes.
        /// </summary>
        public IList<EMobileClass> StudentClass { get; set; }
        /// <summary>
        /// Gets or sets the Student Section.
        /// </summary>
        public IList<EMobileSection> StudentSections { get; set; }
        /// <summary>
        /// Gets or sets the Student Faculty.
        /// </summary>
        public IList<EMobileFaculty> StudentFaculty { get; set; }
        /// <summary>
        /// Gets or sets the Student Subject.
        /// </summary>
        public IList<EMobileSubject> StudentSubject { get; set; }
        /// <summary>
        /// Gets or sets the Student Stream.
        /// </summary>
        public IList<EMobileStream> StudentStream { get; set; }
        /// <summary>
        /// Gets or sets the student Parent.
        /// </summary>
        public IList<EMobileParent> StudentParent { get; set; }
    }
}
