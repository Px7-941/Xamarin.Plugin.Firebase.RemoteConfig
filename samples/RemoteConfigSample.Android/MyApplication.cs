using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Firebase;
using Plugin.CurrentActivity;

namespace RemoteConfigSample.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
            FirebaseApp.InitializeApp(this);
        }

        public void OnActivityCreated(Activity activity, Bundle? savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }
    }
}
