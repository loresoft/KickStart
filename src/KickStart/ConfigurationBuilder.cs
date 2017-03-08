using System;
using System.Collections.Generic;
using System.Reflection;

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
        /// <exception cref="ArgumentNullException"><paramref name="configuration"/> is <c>null</c>.</exception>
        public ConfigurationBuilder(Configuration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

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
        /// Include the current loaded assemblies as a type scaning source.
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
        /// Include the specified <see cref="Assembly" /> as a type scaning source.
        /// </summary>
        /// <param name="assembly">The assembly to include.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public IConfigurationBuilder IncludeAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            Configuration.Assemblies.IncludeAssembly(assembly);
            return this;
        }

        /// <summary>
        /// Include the assemblies that contain the specified name as a type scaning source.
        /// </summary>
        /// <param name="name">The name to compare.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
        public IConfigurationBuilder IncludeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Configuration.Assemblies.IncludeName(name);
            return this;
        }


        /// <summary>
        /// Exclude the specified <see cref="Assembly" /> as a type scaning source.
        /// </summary>
        /// <param name="assembly">The assembly to exclude.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public IConfigurationBuilder ExcludeAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            Configuration.Assemblies.ExcludeAssembly(assembly);
            return this;
        }

        /// <summary>
        /// Exclude the assemblies that start with the specified name as a type scaning source.
        /// </summary>
        /// <param name="name">The name to compare.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
        public IConfigurationBuilder ExcludeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Configuration.Assemblies.ExcludeName(name);
            return this;
        }


        /// <summary>
        /// Sets the context data element with the specified <paramref name="key" />.
        /// </summary>
        /// <param name="key">The key of the element to set.</param>
        /// <param name="value">The value for specified key.</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to configure KickStart.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public IConfigurationBuilder Data(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _configuration.Data[key] = value;
            return this;
        }

        /// <summary>
        /// Configure the context data with the specified <paramref name="data" /> <see langword="delegate" />.
        /// </summary>
        /// <param name="data">The <see langword="delegate" /> to configure context data.</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to configure KickStart.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <c>null</c>.</exception>
        public IConfigurationBuilder Data(Action<IDictionary<string, object>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data(_configuration.Data);
            return this;
        }


        /// <summary>
        /// Set a <see langword="delegate" /> where log messages will be written.
        /// </summary>
        /// <param name="writer">The writer <see langword="delegate" />..</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to configure KickStart.
        /// </returns>
        public IConfigurationBuilder LogTo(Action<string> writer)
        {
            Configuration.LogWriter = writer;
            return this;
        }


        /// <summary>
        /// Run the specified <see cref="IKickStarter"/> extension on startup.
        /// </summary>
        /// <param name="starter">The <see cref="IKickStarter"/> extension to run.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="starter"/> is <c>null</c>.</exception>
        public IConfigurationBuilder Use(IKickStarter starter)
        {
            if (starter == null)
                throw new ArgumentNullException(nameof(starter));

            _configuration.Starters.Add(starter);
            return this;
        }

    }
}