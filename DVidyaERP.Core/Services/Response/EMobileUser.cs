//Resposne MobileUser data user by Services
using System;
namespace DVidyaERP.Core.Services.Response
{
    public  class EMobileUserUser
    {
        #region Properties
        public  int Code { get; set; }
        public  string Name { get; set; }
        public  string Mobile { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public  bool Status { get; set; }
        public  string DeviceId { get; set; }
        public  bool DeviceActivated { get; set; }
        public  bool DCRequested { get; set; }     
        public  string DCRequestDateString { get; set; }
        public  bool Configured { get; set; }
        #endregion

    }
}