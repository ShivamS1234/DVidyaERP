using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using DVidyaERP;
using DVidyaERP.Global_Method_Propertise;
using DVidyaERP.Helpers;
using DVidyaERP.Core.Models.Tables;
#if __ANDROID__
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
#endif

using Plugin.Settings;
using Xamarin.Forms;

using DVidyaERP.Core.Services.Request;
//using Xamarin.Media;

namespace DVidyaERP
{
    public class ProfileViewModel : BaseViewModel
    {
        // to hold the byte array of selected image in Profile page
        public static byte[] imageByteArray_ViewModel;

        public LogInTable  myProfile;
        public static int id { get; set; }

        public ProfileViewModel()
        {
            Title = "Fitness";
            Icon = "icon.png";
            EditCommand = new Command(OnEditCommand);

        }


        ImageSource _imageSource { get; set; }
        public ImageSource imageSource
        {
            get { return _imageSource; }
            set
            {
                if (value != _imageSource)
                {
                    OnPropertyChanged(nameof(imageSource));
                }
            }
        }

        string emailID = string.Empty;
        public const string emailIDPropertyName = "DisplayemailID";
        public string DisplayemailID
        {
            get { return emailID; }
            set { SetProperty(ref emailID, value, emailIDPropertyName); }
        }

        string displayName = string.Empty;
        public const string displayNamePropertyName = "DisplayName";
        public string DisplayName
        {
            get { return displayName; }
            set { SetProperty(ref displayName, value, displayNamePropertyName); }
        }

        string address = string.Empty;
        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value, nameof(Address)); }
        }

        string mobileno = string.Empty;
        public string MobileNo
        {
            get { return mobileno; }
            set { SetProperty(ref mobileno, value, nameof(MobileNo)); }
        }

        //here we wite auto call system
        public void CallInit()
        {
            
        }
        //end

        //not use full for its time
        string avatar = string.Empty;
        public const string AvatarPropertyName = "Avatar";
        public string Avatar
        {
            get { return avatar; }
            set { SetProperty(ref avatar, value, AvatarPropertyName); }
        }
        //end

#region declare_here_event
        //when we click on the edit command 
        public ICommand EditCommand { protected set; get; }
        public void OnEditCommand()
        {
            try
            {
                //here we write method for Edit profile screen and other propertise   

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //end

        public async Task ExecuteGetCPFeedCommand()
        {
            try
            {
                var loginCount = App.Database.GetLogInAsync().Result;
                if (loginCount != null)
                {
                    myProfile = loginCount;
                    id = myProfile.Code;
                    DisplayemailID = myProfile.Email;
                    DisplayName = myProfile.Name;
                    MobileNo = myProfile.Mobile;
                    Address = myProfile.Address1;
                    imageSource = ImageSource.FromFile("logo.png");  
                }
                else
                {
                    id = CrossSettings.Current.GetValueOrDefault("FacultyId", 0);
                    DisplayemailID = "Demo@DVidya.com";
                    DisplayName ="DVidya ERP";
                    Address = "Noida, India";
                    imageSource = ImageSource.FromFile("logojpg.jpg");
                }

                //this.Avatar = myProfile.avatar;
                //this.emailID = myProfile.email;
                //Profile.Add(myProfile);
                //avatar.Add(profiles.avatar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // for upload profile image in profile page 

        public void SelectPhoto()
        {
            UserDialogs.Instance.ShowLoading();
            var config = new ActionSheetConfig();
            config.Add("Take Photo", async () => { await TakePhoto(); });
            config.Add("Choose from library", async () => { await galleryFunc(); });
            config.SetCancel("Cancel");
            Acr.UserDialogs.UserDialogs.Instance.ActionSheet(config);
            UserDialogs.Instance.HideLoading();
        }

        // take the photo from camera to add it on profile pic
        private async Task TakePhoto()
        {
            try
            {
                if (await checkPermissions())
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        UserDialogs.Instance.Alert("No Camera", ":( No camera avaialble.", "OK");
                        return;
                    }
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        AllowCropping = true,
                        CompressionQuality = 80,
                        Directory = "Sample",
                        Name = "UserImage.jpg"
                    });

                    if (file == null)
                        return;
                    else
                    {
                        UserDialogs.Instance.ShowLoading("Uploading Image...", MaskType.Black);
                        var memoryStream = new MemoryStream();
                        file.GetStream().CopyTo(memoryStream);
                        imageByteArray_ViewModel = memoryStream.ToArray();
                        UserDialogs.Instance.HideLoading();

                        UserDialogs.Instance.ShowLoading("Uploading Image...", MaskType.Black);

                        imageByteArray_ViewModel = memoryStream.ToArray();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            imageSource = ImageSource.FromStream(() => file.GetStream());
                        });
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.Alert("Image Uploaded Successfully.", "Alert", "Ok");
                        //file.Dispose();
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Permissions Denied", "Unable to take photos.", "OK");
                }
            }
            catch (Exception ex)
            {

            }
        }
        // perfrom the action afetr getting the image from galley or camera
        public async Task OnPhotoReceived(string file, string imgPath)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                await Task.Delay(500);
                imageSource = file;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }

        // on select of Gallery
        private async Task galleryFunc()
        {

#if __IOS__
            var picker = new MediaPicker();
            try
            {

                if (!picker.PhotosSupported)
                {
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert("Photo Gallery is unavailable");
                }
                else
                {
                    var result = await picker.PickPhotoAsync();
                    if (result == null)
                        return;
                    else
                    {
                        var memoryStream = new MemoryStream();
                        UserDialogs.Instance.ShowLoading("Uploading Image...", MaskType.Black);
                        result.GetStream().CopyTo(memoryStream);
                        imageByteArray_BlaffrViewModel = memoryStream.ToArray();

                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ProfilePic = ImageSource.FromStream(() => result.GetStream());
                    });
                    UIKit.UIImage image = new UIKit.UIImage(result.Path);
                    UIKit.UIImage highres = image;
                    Foundation.NSData d = highres.AsJPEG(0.7f);
                    Byte[] myByteArray = new Byte[d.Length];
                    System.Runtime.InteropServices.Marshal.Copy(d.Bytes, myByteArray, 0, Convert.ToInt32(d.Length));
                    imageByteArray_BlaffrViewModel = myByteArray;
                    var p = ImageSource.FromStream(() => result.GetStream());
                    await this.OnPhotoReceived(result.Path, "");
                }

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
#endif
#if __ANDROID__
            var picker = new CrossMedia();
            try
            {

                //if (!picker.IsLocalVoiceInteractionSupported)
                //{
                //    UserDialogs.Instance.HideLoading();
                //    UserDialogs.Instance.Alert("Photo Gallery is unavailable");
                //}
                //else
                //{
                //    var result = await picker();
                //    if (result == null)
                //        return;
                //    else
                //    {
                //        var memoryStream = new MemoryStream();
                //        UserDialogs.Instance.ShowLoading("Uploading Image...", MaskType.Black);
                //        result.GetStream().CopyTo(memoryStream);
                //        imageByteArray_ViewModel = memoryStream.ToArray();

                //    }
                //    Device.BeginInvokeOnMainThread(() =>
                //    {
                //        imageSource = ImageSource.FromStream(() => result.GetStream());
                //    });
                //    //UIKit.UIImage image = new UIKit.UIImage(result.Path);
                //    //var highres = image;
                //    //var d = highres.AsJPEG(0.7f);
                //    //Byte[] myByteArray = new Byte[d.Length];
                //    //System.Runtime.InteropServices.Marshal.Copy(d.Bytes, myByteArray, 0, Convert.ToInt32(d.Length));
                //    //imageByteArray_ViewModel = myByteArray;
                //    //var p = ImageSource.FromStream(() => result.GetStream());
                //    await this.OnPhotoReceived(result.Path, "");
                //}

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
#endif
        }

        // to check the permission of user
        private async Task<bool> checkPermissions()
        {
            bool result;
            try
            {
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                    cameraStatus = results[Permission.Camera];
                    storageStatus = results[Permission.Storage];
                }

                if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
                {
                    result = true;
                }
                else
                {
                    //await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                    //On iOS you may want to send your user to the settings screen.
                    result = false;
                    //CrossPermissions.Current.OpenAppSettings();
                }
                return result;
            }
            catch (Exception ex)
            {
                return result = false;
            }
        }

        public async Task<bool> CheckValidation()
        {
            try
            {
                if(string.IsNullOrEmpty(DisplayName))
                {
                    UserDialogs.Instance.Alert("Profile", "Can't blank User Name!.", "OK");
                }
                else if (string.IsNullOrEmpty(emailID))
                {
                    UserDialogs.Instance.Alert("Profile", "Can't blank Email-ID!.", "OK");
                }
                else if (string.IsNullOrEmpty(MobileNo))
                {
                    UserDialogs.Instance.Alert("Profile", "Can't blank Mobile No.!.", "OK");
                }
                else
                {
                    
                    EMobileEditUser _ProfileRequest = new EMobileEditUser()
                    {
                        Address1 = Address,
                        Email = emailID,
                        Mobile = MobileNo,
                        Name = DisplayName,
                        Image = null
                    };
                    EditUserRequest rootObject = new EditUserRequest();
                    rootObject.EditUser = new EMobileEditUser();
                    rootObject.EditUser = _ProfileRequest;
                    rootObject.UserID = CrossSettings.Current.GetValueOrDefault("FacultyId", 0);
                    rootObject.UserType = (int)UserType.currentUserType;
                    var result = GeneralMethod.EditProfile(rootObject);
                    if (result)
                    {
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

#endregion
    }
}