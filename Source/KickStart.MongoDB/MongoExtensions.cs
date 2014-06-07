using System;

namespace KickStart.MongoDB
{
    public static class MongoExtensions
    {
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