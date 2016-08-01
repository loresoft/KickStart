using System;

namespace KickStart.StartupTask
{
    /// <summary>
    /// Base class for start tasks
    /// </summary>
    public abstract class StartupTaskBase : IStartupTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupTaskBase"/> class.
        /// </summary>
        protected StartupTaskBase()
        {
            Priority = 100;
        }

        /// <summary>
        /// Gets the priority of this task. Lower numbers run first.
        /// </summary>
        /// <value>
        /// The priority of this task.
        /// </value>
        public int Priority { get; set; }

        /// <summary>
        /// Runs the startup task.
        /// </summary>
        public abstract void Run();
    }
}