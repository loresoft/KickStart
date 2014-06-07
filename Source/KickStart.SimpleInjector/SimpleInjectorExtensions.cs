using System;

namespace KickStart.SimpleInjector
{
    public static class SimpleInjectorExtensions
    {
        public static ISimpleInjectorBuilder UseSimpleInjector(this IConfigurationBuilder configurationBuilder)
        {
            var options = new SimpleInjectorOptions();
            var starter = new SimpleInjectorStarter(options);
            var builder = new SimpleInjectorBuilder(options);

            configurationBuilder.ExcludeName("SimpleInjector");
            configurationBuilder.Use(starter);

            return builder;
        }
    }
}