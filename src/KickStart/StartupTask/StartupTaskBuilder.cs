using System;

namespace KickStart.StartupTask
{
    /// <summary>
    /// A fluent <see langword="class"/> to configure startup tasks
    /// </summary>
    public class StartupTaskBuilder : IStartupTaskBuilder
    {
        private readonly StartupTaskOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupTaskBuilder"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public StartupTaskBuilder(StartupTaskOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Uses the <see cref="Kick.Container" /> to resolve startup task instances.
        /// </summary>
        /// <param name="value">if set to <c>true</c>, startup task instances will be resolved from the <see cref="Kick.Container" />.</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to configure startup tasks
        /// </returns>
        public IStartupTaskBuilder UseContainer(bool value = true)
        {
            _options.UseContainer = value;
            return this;
        }
    }
}