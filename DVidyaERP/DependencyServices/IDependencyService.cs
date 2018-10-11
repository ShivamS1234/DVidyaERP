using System;
namespace DVidyaERP.DependencyServices
{
    public interface IDependencyService
    {
        string GetNetworkName();
        void CloseApp();    //Method to clost the app. Implemented in platform specific projects
        string DeviceID();
        string getLocalPath(string filename);
    }

}
