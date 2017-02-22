using System;

namespace KickStart.StartupTask
{
    /// <summary>
    /// A fluent <see langword="interface"/> to configure startup tasks
    /// </summary>
    public interface IStartupTaskBuilder
    {
        /// <summary>
        /// Uses the <see cref="Kick.ServiceProvider"/> to resolve startup task instances.
        /// </summary>
        /// <param name="value">if set to <c>true</c>, startup task instances will be resolved from the <see cref="Kick.ServiceProvider"/>.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure startup tasks
        /// </returns>
        IStartupTaskBuilder UseContainer(bool value = true);
    }
}