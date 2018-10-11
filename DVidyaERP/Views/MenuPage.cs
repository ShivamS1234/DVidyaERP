using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using DVidyaERP.Pages;
using DVidyaERP.Global_Method_Propertise;

namespace DVidyaERP.Views
{
	public class MenuTableView : TableView
	{
        
	}

	public class MenuPage : ContentPage
	{
		public ListView Menu { get; set; }
        public RootPage rootPage;
		TableView tableView;
        string strMSG = string.Empty;

        public MenuPage(RootPage rootPage,int _hostMenu=0)
		{
			Icon = "menu.png";
			Title = "menu"; // The Title property must be set.

			this.rootPage = rootPage;

            var logoutButton = new Button { Text = "Logout", TextColor = Color.White ,BackgroundColor=App.BrandColor };
			logoutButton.Clicked += (sender, e) =>
			{
                Application.Current.MainPage = new NavigationPage(new LogInPage()) { BarBackgroundColor = Color.FromHex("#F8A51B"), BarTextColor = Color.White };
                //set menulogout 
                Constants.MenuLogout = true;
			};

			var layout = new StackLayout
			{
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				//BackgroundColor = Color.FromHex("#8DB640"),
                BackgroundColor = App.BrandColor
			};
            var section = new TableSection();
            section.Clear();
            section = new TableSection();
            if (UserType.currentUserType == UserType.enumUserType.Faculty)
            {
                section.Add(new MenuCell { Text = "Take Attendance", Host = this, ImageSrc = "takeAttendanceIcon.png" });  
            }


            section.Add(new MenuCell { Text = "Show Attendance", Host = this, ImageSrc = "showAttendanceIcon.png" });
            section.Add(new MenuCell {Text = "Time Table",Host= this,ImageSrc="timetableicon.png"});
            section.Add(new MenuCell {Text = "Notice Board",Host= this,ImageSrc="noticeicon.png"});
            section.Add(new MenuCell {Text = "Fees",Host= this,ImageSrc="feesicon.png"});
            section.Add(new MenuCell {Text = "Gallery",Host= this,ImageSrc="galleryIcon.png"});
            section.Add(new MenuCell {Text = "Sync Data",Host= this,ImageSrc="synIcon.png"});

            var root = new TableRoot() { section};

            tableView = new MenuTableView()
            {
                Root = root,
                Intent = TableIntent.Data,
                Margin = new Thickness(0, 0, 0, 0),
            };



			var settingView = new SettingsUserView();

			layout.Children.Add(settingView);

			layout.Children.Add(tableView);
			layout.Children.Add(logoutButton);

			Content = layout;

            //set settingView click event
			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped +=
				(sender, e) =>
				{
                    setProfile_Click();
				};
			settingView.GestureRecognizers.Add(tapGestureRecognizer);
            //set profile button delegate
            settingView.btnProfile.Clicked += 
                delegate {
                setProfile_Click();
                };
            if (_hostMenu > 0)
            {
                if (_hostMenu == 1)
                    Selected("Take Attendance");
                else if (_hostMenu == 2)
                    Selected("Show Attendance");
                else if (_hostMenu == 3)
                    Selected("Time Table");
            }

		}
        private void setProfile_Click()
        {
            NavigationPage profile = new NavigationPage(new ProfilePage()) { Title = "Profile", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
            rootPage.Detail = profile;
            rootPage.IsPresented = false; 
        }

		NavigationPage _TakeAttendance,_ShowAttendance, _TimeTable, _NoticeBoard , _Gallery , _Configure;
		public void Selected(string item)
		{
			switch (item)
			{
                case "Take Attendance":
                    if (_TakeAttendance == null)
                        _TakeAttendance = new NavigationPage(new TakeAttendancePage()) {Title = "Take Attendance",BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = _TakeAttendance;
                    UserType.currentAttendanceType = UserType.attendanceType.Take;
					break;
                case "Show Attendance":
                    //DisplayAlert("Show Attendance", "Today Meals Screen Under Construction.", "OK");
                    _ShowAttendance = new NavigationPage(new ShowAttendancePage()) {Title="Show Attendance", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = _ShowAttendance;
                    UserType.currentAttendanceType = UserType.attendanceType.Show;
					break;
                case "Time Table":
                    //DisplayAlert("Time Table", "Time Table Under Construction.", "OK");
                    _TimeTable = new NavigationPage(new TimeTablePage()) { Title = "Time Table", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = _TimeTable;
					break;
                case "Notice Board":
                    //DisplayAlert("Notice Board", "Notice Board Under Construction.", "OK");
                    _NoticeBoard = new NavigationPage(new NoticePage()) { Title = "Notice Board", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = _NoticeBoard;
                    break;
                case "Fees":
                    //DisplayAlert("Notice Board", "Notice Board Under Construction.", "OK");
                    _NoticeBoard = new NavigationPage(new DemoPage()) { Title = "Fees", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = _NoticeBoard;
                    break;
                case "Gallery":
                    //DisplayAlert("Gallery", "Gallery Under Construction.", "OK");
                    _Gallery = new NavigationPage(new DemoPage()) { Title = "Gallery", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = _Gallery;
                    break;
                case "Sync Data":
                    //DisplayAlert("Configure", "Configure Under Construction.", "OK");
                    _Configure = new NavigationPage(new ConfigurePage()) { Title = "Configure", BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = _Configure;
                    break;
			};
			rootPage.IsPresented = false;  // close the slide-out
		}

        async void UpdateLogOut(string email)
        {
            try
            {
                //App.Database.UpdateLogInAuthAsync(AuthID:"", Email:email);
                ////set AuthLoginToken
                //Settings.AuthLoginToken = "";
            }
            catch (Exception ex)
            {

            }
        }

	}
}
