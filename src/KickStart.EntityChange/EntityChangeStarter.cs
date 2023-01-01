using System;

namespace KickStart.EntityChange
{
    /// <summary>
    /// A KickStart extension to initialize EntityChange.
    /// </summary>
    public class EntityChangeStarter : IKickStarter
    {
        private readonly EntityChangeOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityChangeStarter"/> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public EntityChangeStarter(EntityChangeOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var profiles = context.GetInstancesAssignableFrom<global::EntityChange.IEntityProfile>();

            var configuration = global::EntityChange.EntityConfiguration.Default;
            configuration.Configure(c =>
            {
                foreach (var profile in profiles)
                {
                    context.WriteLog("EntityChange Profile: {0}", profile);
                    c.Profile(profile);
                }
            });

            if (_options.Configure != null)
                configuration.Configure(_options.Configure);

        }
    }
}