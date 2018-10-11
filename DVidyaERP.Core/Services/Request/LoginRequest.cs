using System;
using System.Collections.Generic;

namespace DVidyaERP.Core.Services.Request
{
    //for Login
    public class LoginRequest
    {

        public String Email;

        public String Password;

        public String DeviceID;

        public int UserType;
    }
    //For Configure Request
    public class DownloadUserMetadataRequest
    {
        public long UserID { get; set; }
        public int UserType { get; set; }
    }
    //end

    //For Edit Profile request 
    public class EMobileEditUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// get or set User Image.
        /// </summary>
        public byte[] Image { get; set; }
    }
    public class EditUserRequest
    {
        public long UserID { get; set; }
        public EMobileEditUser EditUser { get; set; }
        public int UserType { get; set; }
    }
    //end

    //For Edit Profile response
    public class ProfileResponse
    {
        public bool Updated { get; set; }
        public string Status { get; set; }
    }
    //end
}