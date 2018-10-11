using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;
using DVidyaERP.DependencyServices;
using DVidyaERP.Droid.DependencyService_Android;
using Plugin.Settings;

namespace DVidyaERP.Droid.DependencyService_Android
{
    public interface IDependencyServiceForAutoRun
    {
         void SyncAttendanceData(int userID, int UserType);  
         void SyncTimeTableData(int userID, int UserType);   
    }

}

namespace DVidyaERP.Droid 
{
    [Service]
    public class ERPservice : Service
    {
        //service name
        static readonly string TAG = "X:" + typeof(ERPservice).Name;
        //service timer
        //static readonly int TimerWait = 60000;
        //for testing perid only
        static readonly int TimerWait = 13000;
        Timer timer;

        DateTime startTime;
        bool isStarted = false;
        public override void OnCreate()
        {
            base.OnCreate();
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug(TAG, $"OnStartCommand called at {startTime}, flags={flags}, startid={startId}");
            Toast.MakeText(this, $"OnStartCommand called at {startTime}, flags={flags}, startid={startId}", ToastLength.Short).Show();
            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                Log.Debug(TAG, $"This service was already started, it's been running for {runtime:c}.");
                Toast.MakeText(this, $"This service was already started, it's been running for {runtime:c}.", ToastLength.Short).Show();
            }
            else
            {
                startTime = DateTime.UtcNow;
                Log.Debug(TAG, $"Starting the service, at {startTime}.");
                Toast.MakeText(this, $"Starting the service, at {startTime}.", ToastLength.Short).Show();
                timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;
            }
            return StartCommandResult.NotSticky;
        }
        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.            
            return null;
        }
        public override void OnDestroy()
        {
            //timer.Dispose(); 
            //timer = null; 
            //isStarted = true;
            //TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
            //Log.Debug(TAG, $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.");
            //Toast.MakeText(this, $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.", ToastLength.Short).Show();
            //base.OnDestroy();
        }
        /// <summary>
        /// call service method
        /// </summary>
        /// <param name="state"></param>
        void HandleTimerCallback(object state)
        {
            try
            {
                TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
                    //check here for user sync data
                    var id = CrossSettings.Current.GetValueOrDefault("FacultyId", 0);
                    var userType = CrossSettings.Current.GetValueOrDefault("UserType", 0);
                    int userid = 0;
                    //int.TryParse(id, out userid);
                    if (id > 0 && userType > 0)
                    {
                        //set service method here
                        //call attendance data method
                        if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                        {
                        DependencyService.Get<IDependencyServiceForAutoRun>().SyncAttendanceData(id,userType);

                            //call Time Table method
                        DependencyService.Get<DependencyServiceForAutoRun>().SyncTimeTableData(id, userType);
                        }
                    }

                Log.Debug(TAG, $"This service has been running for {runTime:c} (since ${state}).");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Debug(TAG, $"error in the service please check.. stack trace {ex.Message} .");
            }

            //after calling view result
            //Toast.MakeText(this, "service is running time : " + runTime, ToastLength.Short).Show();

        }
    }
}