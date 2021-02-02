using Plugin.FirebaseRemoteConfig;
using Xamarin.Forms;

namespace RemoteConfigSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var defaultConfigResourceName = "remote_config_defaults";
#if DEBUG
            var minimumFetchIntervalInSeconds = 1L;
            CrossFirebaseRemoteConfig.Current.Init(minimumFetchIntervalInSeconds, defaultConfigResourceName: defaultConfigResourceName);
#else // Use defaults
            CrossFirebaseRemoteConfig.Current.Init(defaultConfigResourceName: defaultConfigResourceName);
#endif

            MainPage = new MainPage();
        }
    }
}
