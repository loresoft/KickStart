using System;
using KickStart.MongoDB;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for MongoDB.
    /// </summary>
    public static class MongoExtensions
    {
        /// <summary>
        /// Use the KickStart extension to configure MongoDB.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public static IConfigurationBuilder UseMongoDB(this IConfigurationBuilder configurationBuilder)
        {
            var options = new MongoOptions();
            var starter = new MongoStarter(options);
            var builder = new MongoBuilder(options);

            configurationBuilder.ExcludeName("MongoDB");
            configurationBuilder.Use(starter);

            return configurationBuilder;
        }
    }
}