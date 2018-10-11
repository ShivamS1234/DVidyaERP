using System;
using Xamarin.Forms;
using DVidyaERP.Pages;
using DVidyaERP.Global_Method_Propertise;
using Plugin.Settings;
using DVidyaERP.ViewModels;

namespace DVidyaERP.Views
{
	public class RootPage : MasterDetailPage
	{
		MenuPage menuPage;

        public RootPage(int _hostMenu=0)
		{
            NavigationPage.SetHasNavigationBar(this, false);
			NavigationPage.SetHasBackButton(this, false);
            menuPage = new MenuPage(this,_hostMenu);
			Master = menuPage;
           
            if (ConfigruePageViewModel.StatusStudent ==false )
            { 
               Detail = new NavigationPage(new ConfigurePage()) { Title = "Configure", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };    
            }
            else
            {
                if (UserType.currentUserType == UserType.enumUserType.Faculty)
                {
                    Detail = new NavigationPage(new TakeAttendancePage()) { Title = "Take Attendance", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                }
                else
                {
                    Detail = new NavigationPage(new ShowAttendancePage()) { Title = "Show Attendance", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                }
            }
		}
   
	}
}
