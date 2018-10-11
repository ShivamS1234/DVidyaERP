using System;
using System.Collections.ObjectModel;
using DVidyaERP.CustomControl;
using Xamarin.Forms;
using System.Collections.Generic;
using DVidyaERP.Helpers;
using DVidyaERP.Models;
using DVidyaERP.ViewModels;
using Rg.Plugins.Popup.Extensions;
using DVidyaERP.Global_Method_Propertise;
using Acr.UserDialogs;

namespace DVidyaERP.Pages
{
    public class TakeAttendancePage : ContentPage
    {
        public static SelectableObservableCollection<ItemCell> listitems { get; set; }
        public static SelectableObservableCollection<string> liststring { get; set; }
        private TakeAttendanceViewModel _takeAttendanceViewModel;
        public ListView listView;
        public TakeAttendancePage()
        {
            //declare here date time picker
            try
            {
                TakeAttendanceViewModel.AttendanceType = UserType.attendanceType.Take;
                _takeAttendanceViewModel = new TakeAttendanceViewModel();
                double _imageFOBY = 0.9;
                this.Title = "Take Attendance";
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
                datePicker.Date = _takeAttendanceViewModel.Date;
                datePicker.SetBinding(DatePicker.DateProperty, new Binding("Date"));
                //declare here refresh button
                var refreshButton = new ImageButtonRefresh()
                {
                    Source = ImageSource.FromFile("RefreshIcon.png"),
                    //Margin = new Thickness(15),
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.Transparent,
                    Aspect = Aspect.Fill,

                };
                //declare here submitt button
                var SubmittButton = new Button()
                {
                    Margin = new Thickness(0, 0, 0, 3),
                    BackgroundColor = App.BrandColor,
                    BorderWidth = 1,
                    BorderColor = Color.White,
                    BorderRadius = 20,
                    TextColor = Color.White,
                    Text = "Take",
                    FontFamily = App.fontFamilyHead,
                    WidthRequest = 300,
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Command=_takeAttendanceViewModel.SubmitCommand
                };
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
                //here we are declare tap gesuture

                var tapgesture = new TapGestureRecognizer();
                tapgesture.Tapped += (sender, e) =>
                {
                    OnImageClicked_ChangeMeals();
                };
                tapgesture.NumberOfTapsRequired = 1;
                FOBButton.GestureRecognizers.Add(tapgesture);
                //end
                listView = new ListView()
                {
                    BackgroundColor = Color.DarkGray,
                    HasUnevenRows = true,
                };
                IList<ItemCell> _itemList = new List<ItemCell>();

               // _itemList.Add(new ItemCell { ImageUri = sname, id = "10102", name = "Shivam Singh" });
                //_itemList.Add(new ItemCell { ImageUri = "logo.png", id = "10103", name = "Sonu Nagar" });
                //_itemList.Add(new ItemCell { ImageUri = "workprogress.png", id = "10104", name = "Arun Singh" });


                //DataBindation.listModelAttendance;

                //listitems = new SelectableObservableCollection<ItemCell>(_itemList);
                //listView.ItemsSource = listitems;

                listView.ItemsSource=_takeAttendanceViewModel.StudentList;
                listView.ItemTemplate = new DataTemplate(typeof(SelectableViewCell));
                listView.IsPullToRefreshEnabled = true;
                listView.VerticalOptions = LayoutOptions.Fill;
                //listView.HasUnevenRows =true;
                listView.Refreshing += OnRefresh;
                listView.ItemTapped += OnTapped;
                listView.ItemSelected += OnSelected;

                MultiSelectListView.SetIsMultiSelect(listView, true);

                AbsoluteLayout.SetLayoutFlags(listView, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(listView, Rectangle.FromLTRB(0, 0, 1, 1));

                AbsoluteLayout.SetLayoutFlags(FOBButton, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(FOBButton, new Rectangle(1.0, _imageFOBY, -1, -1));

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
                grid.Children.Add(SubmittButton, 0, 2);
                Grid.SetColumnSpan(SubmittButton, 2);
                Content = new StackLayout
                {
                    Children = {
                    grid
                }
                };

                BindingContext = _takeAttendanceViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Toast(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TakeAttendanceViewModel.AttendanceType = UserType.attendanceType.Take;
            if (MasterChooseDetails.ClassMaster_Code == 0 || MasterChooseDetails.SectionMaster_Code == 0 ||   MasterChooseDetails.StreamMaster_Code == 0)
            {
                OnImageClicked_ChangeMeals();
            }
        }

        #region private_method
        async void OnImageClicked_ChangeMeals()
        {
            var page = new GetStudentsPagePopUp();
            await Navigation.PushPopupAsync(page);
        }

        void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            if (_takeAttendanceViewModel.StudentList.Count > 0)
            {
                 TakeAttendanceViewModel.bindAttendanceData();
            }
            list.IsRefreshing = false;
        }
        void OnTapped(object sender, EventArgs e)
        {
            var list = (ListView)sender;


        }
        void OnSelected(object sender,EventArgs e)
        {
            var list = (ListView)sender;

        }
        #endregion
    }
}

