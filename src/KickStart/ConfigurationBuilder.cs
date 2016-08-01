using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using KickStart.Logging;

namespace KickStart
{
    /// <summary>
    /// A fluent <see langword="class"/> to configure KickStart.
    /// </summary>
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private readonly Configuration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationBuilder"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ConfigurationBuilder(Configuration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <value>
        /// The current configuration.
        /// </value>
        public Configuration Configuration
        {
            get { return _configuration; }
        }


        /// <summary>
        /// Include the current loaded assemblies as a source.
        /// </summary>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder IncludeLoadedAssemblies()
        {
            Configuration.Assemblies.IncludeLoadedAssemblies();
            return this;
        }

        /// <summary>
        /// Include the assembly from the specified type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to get assembly from.</typeparam>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder IncludeAssemblyFor<T>()
        {
            return IncludeAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Include the specified <see cref="Assembly" />.
        /// </summary>
        /// <param name="assembly">The assembly to include.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder IncludeAssembly(Assembly assembly)
        {
            Configuration.Assemblies.IncludeAssembly(assembly);
            return this;
        }

        /// <summary>
        /// Include the assemblies that contain the specified name.
        /// </summary>
        /// <param name="name">The name to compare.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder IncludeName(string name)
        {
            Configuration.Assemblies.IncludeName(name);
            return this;
        }


        /// <summary>
        /// Exclude the assembly from the specified type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to get assembly from.</typeparam>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder ExcludeAssemblyFor<T>()
        {
            return ExcludeAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Exclude the specified <see cref="Assembly" />.
        /// </summary>
        /// <param name="assembly">The assembly to exclude.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder ExcludeAssembly(Assembly assembly)
        {
            Configuration.Assemblies.ExcludeAssembly(assembly);
            return this;
        }

        /// <summary>
        /// Exclude the assemblies that start with the specified name.
        /// </summary>
        /// <param name="name">The name to compare.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder ExcludeName(string name)
        {
            Configuration.Assemblies.ExcludeName(name);
            return this;
        }

        /// <summary>
        /// Set a <see langword="delegate" /> where log messages will be written. This override the default trace logging.
        /// </summary>
        /// <param name="writer">The writer <see langword="delegate" />..</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder LogTo(Action<LogData> writer)
        {
            Logger.RegisterWriter(writer);
            return this;
        }


        /// <summary>
        /// Run the specified <see cref="IKickStarter"/> extension on startup.
        /// </summary>
        /// <param name="starter">The <see cref="IKickStarter"/> extension to run.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder Use(IKickStarter starter)
        {
            _configuration.Starters.Add(starter);
            return this;
        }

    }
}