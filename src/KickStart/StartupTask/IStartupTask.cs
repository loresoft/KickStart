using System;

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
        /// Runs the startup task.
        /// </summary>
        void Run();
    }
}
