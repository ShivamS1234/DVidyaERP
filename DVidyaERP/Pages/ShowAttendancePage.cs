using System;
using System.Collections.ObjectModel;
using DVidyaERP.CustomControl;
using Xamarin.Forms;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using DVidyaERP.ViewModels;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Models;

namespace DVidyaERP.Pages
{
    public class ShowAttendancePage : ContentPage
    {
        public static SelectableObservableCollection<ItemCell> listitems { get; set; }
        public static SelectableObservableCollection<string> liststring { get; set; }
        private TakeAttendanceViewModel _ShowAttendanceViewModel;

        public ShowAttendancePage()
        {
            //declare here date time picker
            TakeAttendanceViewModel.AttendanceType = UserType.attendanceType.Show;
            _ShowAttendanceViewModel = new TakeAttendanceViewModel();
            try
            {
                double _imageFOBY = 0.9;
                this.Title = "Show Attendance";
                //switch (Device.RuntimePlatform)
                //{
                //    case Device.iOS:
                //        _Toppadding = 5;
                //        break;
                //    default:
                //        _imageFOBY = 0.9;
                //        break;
                //}

                var datePicker = new DatePicker()
                {
                    //Date = DateTime.Now,
                    BackgroundColor = Color.White,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Format = "dd/MMM/yyyy",
                    WidthRequest = 130
                };
                //datePicker.SetBinding(DatePicker.DateProperty, new Binding("Date"));
                datePicker.Date = _ShowAttendanceViewModel.Date;
                //declare here refresh button
                var refreshButton = new ImageButtonRefresh()
                {
                    Source = ImageSource.FromFile("RefreshIcon.png"),
                    //Margin = new Thickness(15),
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.Transparent,
                    Aspect = Aspect.Fill,

                };
                //declare here total present & total absent
                var totalPresentANDtotalabsent = new Label()
                {
                    Margin = new Thickness(0, 0, 0, 3),
                    BackgroundColor = App.BrandColor,
                    TextColor = Color.White,
                    Text = "Total Present & Total absent",
                    FontFamily = App.fontFamilyHead,
                    WidthRequest = 300,
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                totalPresentANDtotalabsent.Text = _ShowAttendanceViewModel.Total;
                //declare here FOB Button
                var FOBButton = new ImageButtonFOB()
                {
                    Source = ImageSource.FromFile("iconFOB.png"),
                    Margin = new Thickness(15),
                    VerticalOptions = LayoutOptions.EndAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    BackgroundColor = Color.Transparent,
                    Aspect = Aspect.Fill,
                    WidthRequest = 72,
                    HeightRequest = 72
                };
                var tapgesture = new TapGestureRecognizer();
                tapgesture.Tapped += (sender, e) =>
                {
                    OnImageClicked_ChangeMeals();
                };

                tapgesture.NumberOfTapsRequired = 1;
                FOBButton.GestureRecognizers.Add(tapgesture);

                ListView listView = new ListView()
                { 
                    BackgroundColor = Color.DarkGray,
                    HasUnevenRows = true,
                };
                //IList<ItemCell> _itemList = new List<ItemCell>();
                //_itemList.Add(new ItemCell { ImageUri = "shivam.png", id = "10102", name = "Shivam Singh" ,status="Present" });
                ////_itemList.Add(new ItemCell { ImageUri = "logo.png", id = "10103", name = "Sonu Nagar", status = "Absent" });
                //_itemList.Add(new ItemCell { ImageUri = "workprogress.png", id = "10104", name = "Arun Singh", status = "Present" });

                //listView.ItemsSource = new ObservableCollection<ItemCell>(_itemList);

                listView.ItemsSource=_ShowAttendanceViewModel.StudentListShow;

                listView.ItemTemplate = new DataTemplate(typeof(NonSelectableViewCell));
                totalPresentANDtotalabsent.Text = Global_Method_Propertise.Static_method.getTotalPresentandAbsentStudent(_ShowAttendanceViewModel.StudentListShow);

                //listView.ItemsSource = listitems;
                listView.IsPullToRefreshEnabled = true;
                listView.VerticalOptions = LayoutOptions.Fill;
                listView.Refreshing += OnRefresh;
                listView.ItemTapped += OnTapped;
                listView.ItemSelected += OnSelected;

                AbsoluteLayout.SetLayoutFlags(listView, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(listView, Rectangle.FromLTRB(0, 0, 1, 1));

                AbsoluteLayout.SetLayoutFlags(FOBButton, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(FOBButton, new Rectangle(1.0, _imageFOBY, -1, -1));

                var tapgestureRefreshButton = new TapGestureRecognizer();
                tapgestureRefreshButton.Tapped += (sender, e) =>
                {
                    _ShowAttendanceViewModel.Date = datePicker.Date;
                    TakeAttendanceViewModel.bindShowAttendanceData();
                    totalPresentANDtotalabsent.Text = Global_Method_Propertise.Static_method.getTotalPresentandAbsentStudent(_ShowAttendanceViewModel.StudentListShow);
                };
                tapgestureRefreshButton.NumberOfTapsRequired = 1;
                refreshButton.GestureRecognizers.Add(tapgestureRefreshButton);

                var absoluteLayout = new AbsoluteLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(0, 0, 0, 0),
                    Children = { listView, FOBButton }
                };

                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(500, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

              
                grid.Children.Add(datePicker, 0, 0);
                Grid.SetColumnSpan(datePicker, 2);
                grid.Children.Add(refreshButton, 1, 0);
                grid.Children.Add(absoluteLayout, 0, 1);
                Grid.SetColumnSpan(absoluteLayout, 2);
                grid.Children.Add(totalPresentANDtotalabsent, 0, 2);
                Grid.SetColumnSpan(totalPresentANDtotalabsent, 2);
                Content = new StackLayout
                {
                    Children = {
                    grid
                }
                };
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TakeAttendanceViewModel.AttendanceType = UserType.attendanceType.Show;
            if (MasterChooseDetails.ClassMaster_Code == 0 || MasterChooseDetails.SectionMaster_Code == 0 || MasterChooseDetails.StreamMaster_Code == 0)
            {
                OnImageClicked_ChangeMeals();
            }
        }

        #region private_method
        void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            list.IsRefreshing = false;
        }
        void OnTapped(object sender, EventArgs e)
        {
            var list = (ListView)sender;


        }
        void OnSelected(object sender, EventArgs e)
        {
            var list = (ListView)sender;

        }

        async void OnImageClicked_ChangeMeals()
        {
            var page = new GetStudentsPagePopUp();
            await Navigation.PushPopupAsync(page);
        }
        #endregion
    }
}

