using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using KickStart.Logging;

namespace KickStart
{
    /// <summary>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </summary>
    public interface IConfigurationBuilder
    {
        /// <summary>
        /// Include the <see cref="AppDomain"/> loaded assemblies as a source.
        /// </summary>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder IncludeLoadedAssemblies();


        /// <summary>
        /// Include the assembly from the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to get assembly from.</typeparam>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder IncludeAssemblyFor<T>();

        /// <summary>
        /// Include the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly to include.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder IncludeAssembly(Assembly assembly);


        /// <summary>
        /// Include the assemblies that contain the specified name.
        /// </summary>
        /// <param name="name">The name to compare.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder IncludeName(string name);

        /// <summary>
        /// Exclude the assembly from the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to get assembly from.</typeparam>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder ExcludeAssemblyFor<T>();

        /// <summary>
        /// Exclude the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly to exclude.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder ExcludeAssembly(Assembly assembly);

        /// <summary>
        /// Exclude the assemblies that start with the specified name.
        /// </summary>
        /// <param name="name">The name to compare.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder ExcludeName(string name);


        /// <summary>
        /// Run the specified IKickStarter extension on startup.
        /// </summary>
        /// <param name="starter">The IKickStarter extension to run.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder Use(IKickStarter starter);

        /// <summary>
        /// Set a <see langword="delegate"/> where log messages will be written. This override the default trace logging.
        /// </summary>
        /// <param name="writer">The writer <see langword="delegate"/>..</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        IConfigurationBuilder LogTo(Action<LogData> writer);
    }
}