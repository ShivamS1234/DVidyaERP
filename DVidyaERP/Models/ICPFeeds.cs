using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVidyaERP
{
    public interface ICPFeeds
    {
        Task Init();


        Task<bool> GetAccessToken(string username, string password);

//      Task<MyProfile> GetMyProfile();

    }

}
