using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Plugin.CurrentActivity;
using Plugin.Firebase.RemoteConfig.Exceptions;
using AndroidFirebaseRemoteConfig = Firebase.RemoteConfig.FirebaseRemoteConfig;
using AndroidFirebaseRemoteConfigSettings = Firebase.RemoteConfig.FirebaseRemoteConfigSettings;

namespace Plugin.FirebaseRemoteConfig
{
    public class FirebaseRemoteConfig : IFirebaseRemoteConfig
    {
        readonly AndroidFirebaseRemoteConfig _config;

        public FirebaseRemoteConfig()
        {
            _config = AndroidFirebaseRemoteConfig.Instance;
        }

        public Task Init() => Init(defaultConfigResourceName: null);

        public async Task Init(long minimumFetchIntervalInSeconds = 12 * 3600, string? defaultConfigResourceName = null)
        {
            var settings = new AndroidFirebaseRemoteConfigSettings.Builder().SetMinimumFetchIntervalInSeconds(minimumFetchIntervalInSeconds).Build();
            await _config.SetConfigSettingsAsync(settings);
            if (!string.IsNullOrWhiteSpace(defaultConfigResourceName))
            {
                var ctx = CrossCurrentActivity.Current.AppContext;
                if (ctx.Resources != null)
                {
                    var resId = ctx.Resources.GetIdentifier(defaultConfigResourceName, "xml", ctx.PackageName);
                    await _config.SetDefaultsAsync(resId);
                }
            }
        }

        public async Task FetchAsync(long cacheExpiration)
        {
            try
            {
                await _config.FetchAsync(cacheExpiration);
            }
            catch (Exception ex)
            {
                throw new FirebaseRemoteConfigFetchFailedException("[FirebaseRemoteConfig] Fetch failed", ex);
            }
        }

        public async Task ActivateAsync() => await _config.Activate();

        public byte[] GetBytes(string key) => _config.GetValue(key).AsByteArray();

        public bool GetBool(string key) => _config.GetBoolean(key);

        public double GetDouble(string key) => _config.GetDouble(key);

        public long GetLong(string key) => _config.GetLong(key);

        public string GetString(string key) => _config.GetString(key);

        public ICollection<string> GetKeysByPrefix(string prefix) => _config.GetKeysByPrefix(prefix);
    }
}
