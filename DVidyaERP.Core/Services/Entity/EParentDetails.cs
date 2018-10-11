using System.Collections.Generic;

namespace DVidyaERP.Core.Services.Entity
{
    
    public class EParentDetails
    {
        /// <summary>
        /// get or sets Parent data
        /// </summary>
        public EMobileParent Parent { get; set; }

        /// <summary>
        /// Gets or sets the Parent Children Classes.
        /// </summary>
        
        public IList<EMobileClass> ParentChildrenClass { get; set; }

        /// <summary>
        /// Gets or sets the Parent Children Section.
        /// </summary>
        
        public IList<EMobileSection> ParentChildrenSections { get; set; }

        /// <summary>
        /// Gets or sets the Parent Children Faculty.
        /// </summary>
        
        public IList<EMobileFaculty> ParentChildrenFaculty { get; set; }

        /// <summary>
        /// Gets or sets the Parent Children Subject.
        /// </summary>
        
        public IList<EMobileSubject> ParentChildrenSubject { get; set; }

        /// <summary>
        /// Gets or sets the ParentChildren Stream.
        /// </summary>
        
        public IList<EMobileStream> ParentChildrenStream { get; set; }

        /// <summary>
        /// Gets or sets the Parent Children.
        /// </summary>
        
        public IList<EMobileStudent> ParentChildren { get; set; }
        
    }
}
