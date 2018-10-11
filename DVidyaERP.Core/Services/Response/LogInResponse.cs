using DVidyaERP.Core.Services.Entity;
using DVidyaERP.Core.Models.Tables;
/// <summary>
/// Declare Login Services Object
/// </summary>
namespace DVidyaERP.Core.Services.Response
{
  public   class LogInResponse
    {
        public  LogInTable MobileUser
        { get; set; }
    }
    
    //Download Response Configure 
    //Faculty data
    public class DownloadFacultyMetadataResponse
    {
        public EFacultyDetails FacultyDetails { get; set; }
    }
    //student data
    public class DownloadStudentMetadataResponse
    {
        public EStudentDetails StudentDetails { get; set; }
    }
    //parent data
    public class DownloadParentMetadataResponse
    {
        public EParentDetails ParentDetails { get; set; }
    }
    
}