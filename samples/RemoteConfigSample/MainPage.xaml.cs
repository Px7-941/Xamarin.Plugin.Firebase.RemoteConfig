using System;
using Plugin.FirebaseRemoteConfig;
using Xamarin.Forms;

namespace RemoteConfigSample
{
    public partial class MainPage : ContentPage
    {
        const string WelcomeMessageKey = "welcome_message";
        const string ShowWelcomeMessageKey = "show_welcome_message";
        const int CacheExpiration = 5;

        IFirebaseRemoteConfig _config;

        public MainPage()
        {
            InitializeComponent();
            _config = CrossFirebaseRemoteConfig.Current;
        }

        async void HandleFetchClicked(object sender, System.EventArgs e)
        {
            try
            {
                await _config.FetchAsync(CacheExpiration);
                await _config.ActivateAsync();
                UpdateMessage();
            }
            catch (Exception ex)
            {
                welcomeLabel.Text = string.Empty;
                errorLabel.Text = ex.Message;
            }
        }

        void UpdateMessage()
        {
            errorLabel.Text = string.Empty;
            if (_config.GetBool(ShowWelcomeMessageKey))
            {
                welcomeLabel.Text = _config.GetString(WelcomeMessageKey);
            }
            else
            {
                welcomeLabel.Text = string.Empty;
            }
        }
    }
}
