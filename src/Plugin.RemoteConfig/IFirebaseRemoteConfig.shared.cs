using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.FirebaseRemoteConfig
{
    public interface IFirebaseRemoteConfig
    {
        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <param name="minimumFetchIntervalInSeconds">Sets the minimum interval between successive fetch calls. Default <c>12h</c>.</param>
        /// <param name="defaultConfigResourceName">If set, load defaults from this resource</param>
        Task Init(long minimumFetchIntervalInSeconds = 12 * 3600, string defaultConfigResourceName = null);

        /// <summary>
        /// Initializes the service without default config.
        /// </summary>
        Task Init();

        /// <summary>
        /// Fetchs the remote config.
        /// </summary>
        /// <param name="cacheExpiration">Cache expiration in seconds.</param>
        /// <exception cref="Firebase.RemoteConfig.Exceptions.FirebaseRemoteConfigFetchFailedException">when fetch fails.</exception>
        Task FetchAsync(long cacheExpiration);

        /// <summary>
        /// Activates the last fetched config.
        /// </summary>
        Task ActivateAsync();

        /// <summary>
        /// Gets the value with specified key as string.
        /// </summary>
        string GetString(string key);

        /// <summary>
        /// Gets the value with specified key as byte array.
        /// </summary>
        byte[] GetBytes(string key);

        /// <summary>
        /// Gets the value with specified key as boolean.
        /// </summary>
        bool GetBool(string key);

        /// <summary>
        /// Gets the value with specified key as long.
        /// </summary>
        long GetLong(string key);

        /// <summary>
        /// Gets the value with specified key as double.
        /// </summary>
        double GetDouble(string key);

        /// <summary>
        /// Gets all keys by prefix.
        /// </summary>
        ICollection<string> GetKeysByPrefix(string prefix);
    }
}
