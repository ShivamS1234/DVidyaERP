using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DVidyaERP.Core.Services;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using DVidyaERP.Models;
using DVidyaERP.ViewModels;
using DVidyaERP.Global_Method_Propertise;

namespace DVidyaERP
{
    public partial class GetStudentsPagePopUp : PopupPage
    {
        #region constructor
        double Calories = 0, Carbs = 0, Fat = 0, Protein = 0;
        int numberOfMeals = 0, _limitRange = 3, _limitValue = 100;
        private readonly GetStudentsPopUpViewModel _viewModel;
        public GetStudentsPagePopUp()
        {
            InitializeComponent();
            _viewModel = new GetStudentsPopUpViewModel();
            BindingContext = _viewModel;
            Opacity = 0.5;

            _viewModel.DisplayInvalidClass += () =>
            {
                DisplayAlert(Constant.ConstantsMSG.Warning, Constant.ConstantsMSG.ClassEmpty_Warning, "OK");
            };
            _viewModel.DisplayInvalidStream += () =>
            {
                DisplayAlert(Constant.ConstantsMSG.Warning, Constant.ConstantsMSG.StreamEmpty_Warning, "OK");
            };
            _viewModel.DisplayInvalidSection += () =>
            {
               DisplayAlert(Constant.ConstantsMSG.Warning, Constant.ConstantsMSG.SubjectEmpty_Warning, "OK");
            };
            _viewModel.CommendClose += () =>
             {
                CloseAllPopup();
             };

            pickerClass.SelectedIndexChanged += (s, e) =>
            {
                _viewModel.ClassName = (PickerItem)((CustomControl.CustomPicker)s).SelectedItem;
            };
            pickerStream.SelectedIndexChanged += (s, e) =>
            {
                _viewModel.StreamName = (PickerItem)((CustomControl.CustomPicker)s).SelectedItem;
            };
            pickerSection.SelectedIndexChanged += (s, e) =>
            {
                _viewModel.SectionName = (PickerItem)((CustomControl.CustomPicker)s).SelectedItem;
            };
        }
        #endregion
        #region private_Method

        #region load_begin

        protected async override Task OnDisappearingAnimationBegin()
        {
            pickerClass.Opacity = pickerStream.Opacity = pickerSection.Opacity = btnOK.Opacity = 0;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            await Task.WhenAll(
                pickerClass.FadeTo(0),
                pickerStream.FadeTo(0),
                pickerSection.FadeTo(0),
                btnOK.FadeTo(0),
                CloseImage.FadeTo(0));
            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });
            await taskSource.Task;
        }
        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            btnOK.Scale = 0.3;
            btnOK.Opacity = 0;


            pickerClass.TranslationX = pickerStream.TranslationX = pickerSection.TranslationX = btnOK.TranslationX = -10;
            pickerClass.Opacity = pickerStream.Opacity = pickerSection.Opacity = btnOK.Opacity = 0;
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            await Task.WhenAll(
                pickerClass.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                pickerClass.FadeTo(1),
                (new Func<Task>(async () =>
                {
                    await Task.Delay(100);
                    await Task.WhenAll(
                    pickerStream.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                    pickerStream.FadeTo(1));
                    await Task.Delay(100);
                    await Task.WhenAll(
                    pickerSection.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                    pickerSection.FadeTo(1));
                    await Task.Delay(100);
                    await Task.WhenAll(
                    btnOK.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                    btnOK.FadeTo(1));
                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                btnOK.ScaleTo(1),
                btnOK.FadeTo(1));
        }


        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
            #endregion
    }
}
