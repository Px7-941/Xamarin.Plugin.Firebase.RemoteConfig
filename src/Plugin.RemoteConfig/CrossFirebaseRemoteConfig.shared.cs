using System;

namespace Plugin.FirebaseRemoteConfig
{
    /// <summary>
    /// Cross platform Firebase RemoteConfig implemenations
    /// </summary>
    public class CrossFirebaseRemoteConfig
    {
        static readonly Lazy<IFirebaseRemoteConfig> Implementation = new Lazy<IFirebaseRemoteConfig>(() => CreateFirebaseRemoteConfig(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current instance to use
        /// </summary>
        public static IFirebaseRemoteConfig Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IFirebaseRemoteConfig CreateFirebaseRemoteConfig()
        {
#if NETSTANDARD2_0
            return null;
#else
            return new FirebaseRemoteConfig();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
