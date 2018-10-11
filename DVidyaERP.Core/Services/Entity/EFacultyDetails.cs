using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DVidyaERP.Core.Services.Entity;

namespace DVidyaERP.Core.Services.Entity
{
    public class EFacultyDetails : EBaseCode
    {
        /// <summary>
        /// 
        /// </summary>
        public EMobileFaculty Faculty { get; set; }

        /// <summary>
        /// Gets or sets the Faculty Classes.
        /// </summary>
        public IList<EMobileClass> FacultyClass { get; set; }

        /// <summary>
        /// Gets or sets the Faculty Classes Transaction.
        /// </summary>
        public IList<EMobileFacultyTransaction> FacultyTransaction { get; set; }

        /// <summary>
        /// Gets or sets the Faculty Section.
        /// </summary>
        public IList<EMobileSection> FacultySections { get; set; }

        /// <summary>
        /// Gets or sets the Faculty Students.
        /// </summary>
        public IList<EMobileStudent> FacultyStudents { get; set; }

        /// <summary>
        /// Gets or sets the FacultySubject.
        /// </summary>
        
        public IList<EMobileSubject> FacultySubject { get; set; }

        /// <summary>
        /// Gets or sets the Faculty Stream.
        /// </summary>
        
        public IList<EMobileStream> FacultyStream { get; set; }

        /// <summary>
        /// Gets or sets the student Parent.
        /// </summary>
        
        public IList<EMobileParent> FacultyStudentParent { get; set; }
    }
}
