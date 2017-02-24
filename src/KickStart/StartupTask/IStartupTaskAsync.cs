using System.Collections.Generic;
using System.Threading.Tasks;

namespace KickStart.StartupTask
{
    /// <summary>
    /// Interface defining an asynchronous task that should run on application startup
    /// </summary>
    /// <remarks>
    /// Asynchronous tasks are run in parallel for the same <see cref="Priority"/>.
    /// </remarks>
    public interface IStartupTaskAsync
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
        Task RunAsync(IDictionary<string, object> data);
    }
}
