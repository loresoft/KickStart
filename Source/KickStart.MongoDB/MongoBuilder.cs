using System;

namespace KickStart.MongoDB
{
    public class MongoBuilder : IMongoBuilder
    {
        private readonly MongoOptions _options;

        public MongoBuilder(MongoOptions options)
        {
            _options = options;
        }
    }
}