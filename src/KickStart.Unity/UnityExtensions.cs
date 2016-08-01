using System;
using KickStart.Unity;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    public static class UnityExtensions
    {
        public static IConfigurationBuilder UseUnity(this IConfigurationBuilder configurationBuilder)
        {
            return UseUnity(configurationBuilder, null);
        }

        public static IConfigurationBuilder UseUnity(this IConfigurationBuilder configurationBuilder, Action<IUnityBuilder> configure)
        {
            var options = new UnityOptions();
            var starter = new UnityStarter(options);

            if (configure != null)
            {
                var builder = new UnityBuilder(options);
                configure(builder);
            }

            configurationBuilder.ExcludeAssemblyFor<UnityStarter>();
            configurationBuilder.ExcludeAssemblyFor<global::Microsoft.Practices.Unity.IUnityContainer>();
            configurationBuilder.Use(starter);

            return configurationBuilder;
        }
    }
}