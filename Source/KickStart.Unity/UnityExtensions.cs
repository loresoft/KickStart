using System;

namespace KickStart.Unity
{
    public static class UnityExtensions
    {
        public static IUnityBuilder UseUnity(this IConfigurationBuilder configurationBuilder)
        {
            var options = new UnityOptions();
            var starter = new UnityStarter(options);
            var builder = new UnityBuilder(options);

            configurationBuilder.ExcludeName("Microsoft.Practices");
            configurationBuilder.Use(starter);

            return builder;
        }
    }
}