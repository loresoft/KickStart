using System;
using System.Diagnostics;
using System.Linq;
using KickStart.Logging;
#if PORTABLE
using Stopwatch = KickStart.Portability.Stopwatch;
#endif

namespace KickStart.StartupTask
{
    /// <summary>
    /// A KickStart extension to run startup tasks on application start.
    /// </summary>
    public class StartupTaskStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<StartupTaskStarter>();
        private readonly StartupTaskOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupTaskStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public StartupTaskStarter(StartupTaskOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var startupTasks = context.GetInstancesAssignableFrom<IStartupTask>(_options.UseContainer)
                .OrderBy(t => t.Priority)
                .ToList();

            var watch = new Stopwatch();

            foreach (var startupTask in startupTasks)
            {

                _logger.Trace()
                    .Message("Execute Startup Task; Type: '{0}'", startupTask)
                    .Write();

                watch.Restart();
                startupTask.Run();
                watch.Stop();

                _logger.Trace()
                    .Message("Complete Startup Task; Type: '{0}', Time: {1} ms", startupTask, watch.ElapsedMilliseconds)
                    .Write();
            }

        }
    }
}