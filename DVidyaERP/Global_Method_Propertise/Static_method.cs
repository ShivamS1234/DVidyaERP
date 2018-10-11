using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using DVidyaERP.CustomControl;

namespace DVidyaERP.Global_Method_Propertise
{
    public static class Static_method
    {
        
        /// <summary>
        /// Exit the application
        /// </summary>
        public static void CloseApplication()
        {
            DependencyService.Get<DependencyServices.IDependencyService>().CloseApp();
        }

        public static string getTotalPresentandAbsentStudent(ObservableCollection<ItemCell> studentList)
        {
            string total=string.Empty;
            if (studentList.Count() > 0)
            {
                var presentcounts = studentList.Where(a => a.status == Constants.Present).Count();
                var absentcounts= studentList.Where(a => a.status == Constants.Absent).Count();

                if (presentcounts > 0)
                {
                    total = "Total Present : " + presentcounts;
                }
                if (absentcounts >0 ) 
                {
                    //var countA = studentList.GroupBy(a => a.status == Constants.Absent).Select(lg => new { PartID = lg.Key, InstanceCount = lg.Count() });
                    //total += "  Total Absent : " + absentcounts.First().InstanceCount;
                    total += "  Total Absent : " + absentcounts;
                }
                return total;
            }
            else
            {
                return "";
            }
        }
    }

}
