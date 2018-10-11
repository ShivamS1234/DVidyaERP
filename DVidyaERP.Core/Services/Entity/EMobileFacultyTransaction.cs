using System.Runtime.Serialization;

namespace DVidyaERP.Core.Services.Entity
{
    public class EMobileFacultyTransaction
    {

        /// <summary>
        /// get or set Faculty Code.
        /// </summary>
        public int FacultyMaster_Code { get; set; }

        /// <summary>
        /// get or set Class Code.
        /// </summary>
        
        public int ClassMaster_Code { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        
        public int SectionMaster_Code { get; set; }

        /// <summary>
        /// get or set SectionCode
        /// </summary>
        
        public int StreamMaster_Code { get; set; }

        /// <summary>
        /// get or set whether this is class incharge or not.
        /// </summary>
        public bool IsClassIncharge { get; set; }
    }
}
