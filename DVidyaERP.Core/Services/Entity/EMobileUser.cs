using DVidyaERP.Core.Global_Method_Propertise;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace DVidyaERP.Core.Services.Entity
{
    
    public class EMobileUser : EBaseCode 
    {
        #region Constructors

        public EMobileUser()
        {
            this.Status = true;
        }

        #endregion

        #region Data Members

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the device id.
        /// </summary>
        
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets whether device is activate or not.
        /// </summary>
        
        public bool DeviceActivated { get; set; }

        /// <summary>
        /// Gets or sets whether device change is requested or not.
        /// </summary>
        
        public bool DCRequested { get; set; }

        /// <summary>
        /// Gets or sets device change request date.
        /// </summary>
        
        public DateTime DCRequestDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DCRequestDateString))
                    return DateTime.MinValue;
                else
                    return DateTime.ParseExact(DCRequestDateString, Propertise.DateTimeFormat, CultureInfo.InvariantCulture);
            }
            set
            {
                DCRequestDateString = value.ToString(Propertise.DateTimeFormat);
            }
        }

        /// <summary>
        /// Gets or sets device change request date.
        /// </summary>
        
        public string DCRequestDateString { get; set; }

        /// <summary>
        /// Gets or sets whether user is cofigured or not.
        /// </summary>
        
        public bool Configured { get; set; }

        #endregion
    }
}
