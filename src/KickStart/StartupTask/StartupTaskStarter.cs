using System;
using System.Linq;
using System.Threading.Tasks;

#if PORTABLE
using Stopwatch = KickStart.Portability.Stopwatch;
#else
using Stopwatch = System.Diagnostics.Stopwatch;
#endif

namespace KickStart.StartupTask
{
    /// <summary>
    /// A KickStart extension to run startup tasks on application start.
    /// </summary>
    public class StartupTaskStarter : IKickStarter
    {
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
            RunAsynchronousTask(context);
            RunActions(context);
        }

        private void RunActions(Context context)
        {
            var watch = Stopwatch.StartNew();
            foreach (var startupTask in _options.Actions)
            {
                context.WriteLog("Execute Startup Action");

                watch.Restart();
                startupTask.Invoke(context.ServiceProvider, context.Data);
                watch.Stop();

                context.WriteLog("Complete Startup Action; Time: {0} ms", watch.ElapsedMilliseconds);
            }
            watch.Stop();
        }

        private void RunAsynchronousTask(Context context)
        {
            // order and group by priority
            var startupGroups = context.GetInstancesAssignableFrom<IStartupTask>()
                .OrderBy(t => t.Priority)
                .GroupBy(p => p.Priority);


            // each priority group is run in parallel 
            foreach (var startGroup in startupGroups)
            {
                context.WriteLog("Execute Startup Tasks; Priority: '{0}'", startGroup.Key);

                // start all tasts for this priority
                var tasks = startGroup
                    .Select(startTask => RunTaskAsync(context, startTask))
                    .ToArray();

                // wait till all done before starting next priority
                Task.WaitAll(tasks);
            }
        }

        private Task RunTaskAsync(Context context, IStartupTask startupTask)
        {
            var watch = Stopwatch.StartNew();
            context.WriteLog("Execute Startup Task; Type: '{0}'", startupTask);

            return startupTask.RunAsync(context.Data)
                .ContinueWith(t =>
                {
                    watch.Stop();
                    context.WriteLog("Complete Startup Task; Type: '{0}', Time: {1} ms", startupTask, watch.ElapsedMilliseconds);
                });
        }
    }
}