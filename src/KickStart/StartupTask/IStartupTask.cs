using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
