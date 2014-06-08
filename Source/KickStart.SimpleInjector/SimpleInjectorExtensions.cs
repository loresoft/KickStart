using System;

namespace KickStart.SimpleInjector
{
    public static class SimpleInjectorExtensions
    {
        public static IConfigurationBuilder UseSimpleInjector(this IConfigurationBuilder configurationBuilder)
        {
            return UseSimpleInjector(configurationBuilder, null);
        }

        public static IConfigurationBuilder UseSimpleInjector(this IConfigurationBuilder configurationBuilder, Action<ISimpleInjectorBuilder> configure)
        {
            var options = new SimpleInjectorOptions();
            var starter = new SimpleInjectorStarter(options);

            if (configure != null)
            {
                var builder = new SimpleInjectorBuilder(options);
                configure(builder);
            }

            configurationBuilder.ExcludeName("SimpleInjector");
            configurationBuilder.Use(starter);

            return configurationBuilder;
        }
    }
}