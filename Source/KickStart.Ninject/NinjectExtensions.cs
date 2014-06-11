using System;

namespace KickStart.Ninject
{
    public static class NinjectExtensions
    {
        public static IConfigurationBuilder UseNinject(this IConfigurationBuilder configurationBuilder)
        {
            return UseNinject(configurationBuilder, null);
        }

        public static IConfigurationBuilder UseNinject(this IConfigurationBuilder configurationBuilder, Action<INinjectBuilder> configure)
        {
            var options = new NinjectOptions();
            var service = new NinjectStarter(options);

            if (configure != null)
            {
                var builder = new NinjectBuilder(options);
                configure(builder);
            }

            configurationBuilder.ExcludeName("Ninject");
            configurationBuilder.Use(service);

            return configurationBuilder;
        }

    }
}