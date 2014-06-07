using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

namespace KickStart.MongoDB
{
    /// <summary>
    /// A KickStart extension to initialize MongoDB.
    /// </summary>
    public class MongoStarter : KickStarter
    {
        private readonly MongoOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoStarter"/> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public MongoStarter(MongoOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public override void Run(Context context)
        {
            var classMaps = GetInstancesAssignableFrom<BsonClassMap>(context);

            foreach (var classMap in classMaps)
            {
                if (BsonClassMap.IsClassMapRegistered(classMap.ClassType))
                    continue;

                Logger.Verbose()
                    .Message("Register MongoDB ClassMap: {0}", classMap)
                    .Write();

                BsonClassMap.RegisterClassMap(classMap);
            }
        }
    }
}
