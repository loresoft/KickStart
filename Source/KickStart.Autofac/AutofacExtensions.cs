using System;

namespace KickStart.Autofac
{
    public static class AutofacExtensions
    {
        public static IConfigurationBuilder UseAutofac(this IConfigurationBuilder configurationBuilder)
        {
            return UseAutofac(configurationBuilder, null);
        }

        public static IConfigurationBuilder UseAutofac(this IConfigurationBuilder configurationBuilder, Action<IAutofacBuilder> configure)
        {
            var options = new AutofacOptions();
            var service = new AutofacStarter(options);

            if (configure != null)
            {
                var builder = new AutofacBuilder(options);
                configure(builder);
            }

            configurationBuilder.ExcludeName("Autofac");
            configurationBuilder.Use(service);

            return configurationBuilder;
        }

    }
}