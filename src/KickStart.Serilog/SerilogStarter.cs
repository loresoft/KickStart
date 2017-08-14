using System;
using Serilog;

namespace KickStart
{
    /// <summary>
    /// A KickStart extension to initialize Serilog.
    /// </summary>
    public class SerilogStarter : IKickStarter
    {
        private readonly SerilogOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogStarter"/> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public SerilogStarter(SerilogOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var configuration = new LoggerConfiguration();

            var configurations = context.GetInstancesAssignableFrom<ISerilogConfiguration>();

            foreach (var c in configurations)
            {
                context.WriteLog("Serilog Configuration: {0}", configuration);
                c.Configure(configuration);
            }

            _options.Configure?.Invoke(configuration);

            // Activate the configuration
            Serilog.Log.Logger = configuration.CreateLogger();
        }
    }
}