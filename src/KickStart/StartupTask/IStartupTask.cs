using System;
using System.Collections.Generic;

namespace KickStart.StartupTask
{
    /// <summary>
    /// Interface defining a task that should run on application startup
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// Gets the priority of this task. Lower numbers run first.
        /// </summary>
        /// <value>
        /// The priority of this task.
        /// </value>
        int Priority { get; }

        /// <summary>
        /// Runs the startup task with the specified context <paramref name="data"/>.
        /// </summary>
        /// <param name="data">The data dictionary shared with all starter modules.</param>
        void Run(IDictionary<string, object> data);
    }
}
