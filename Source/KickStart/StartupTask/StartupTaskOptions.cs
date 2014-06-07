using System;

namespace KickStart.StartupTask
{
    /// <summary>
    /// Startup tasks options class
    /// </summary>
    public class StartupTaskOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to use the <see cref="Kick.Container" /> to resolve startup task instances.
        /// </summary>
        /// <value>
        ///   <c>true</c> to  use the <see cref="Kick.Container" /> to resolve; otherwise, <c>false</c>.
        /// </value>
        public bool UseContainer { get; set; }
    }
}