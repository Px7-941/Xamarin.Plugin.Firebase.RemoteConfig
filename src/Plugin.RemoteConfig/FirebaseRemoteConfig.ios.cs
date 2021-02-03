using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using Plugin.Firebase.RemoteConfig.Exceptions;
using AppleFirebaseRemoteConfig = Firebase.RemoteConfig.RemoteConfig;
using AppleFirebaseRemoteConfigSettings = Firebase.RemoteConfig.RemoteConfigSettings;

namespace Plugin.FirebaseRemoteConfig
{
    public class FirebaseRemoteConfig : IFirebaseRemoteConfig
    {
        AppleFirebaseRemoteConfig _config;

        public FirebaseRemoteConfig()
        {
            _config = AppleFirebaseRemoteConfig.SharedInstance;
        }

        public Task Init() => Init(defaultConfigResourceName: null);

        public Task Init(long minimumFetchIntervalInSeconds = 12 * 3600, string? defaultConfigResourceName = null)
        {
            _config.ConfigSettings = new AppleFirebaseRemoteConfigSettings() { MinimumFetchInterval = minimumFetchIntervalInSeconds };

            if (!string.IsNullOrWhiteSpace(defaultConfigResourceName))
            {
                _config.SetDefaults(defaultConfigResourceName);
            }
            return Task.CompletedTask;
        }

        public async Task FetchAsync(long cacheExpiration)
        {
            var status = await _config.FetchAsync(cacheExpiration);
            if (status != RemoteConfigFetchStatus.Success)
            {
                throw new FirebaseRemoteConfigFetchFailedException($"status: {status}");
            }
        }

        public async Task ActivateAsync() => await _config.ActivateAsync();

        public bool GetBool(string key) => _config[key].BoolValue;

        public byte[] GetBytes(string key) => _config[key].DataValue.ToArray();

        public double GetDouble(string key) => _config[key].NumberValue?.DoubleValue ?? default;

        public long GetLong(string key) => _config[key].NumberValue?.LongValue ?? default;

        public string GetString(string key) => _config[key].StringValue;

        public ICollection<string> GetKeysByPrefix(string prefix)
        {
            return _config.GetKeys(prefix).ToArray().Select(x => x.ToString()).ToList();
        }
    }
}
