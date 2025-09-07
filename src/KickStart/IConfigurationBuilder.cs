using System.Reflection;

namespace KickStart;

/// <summary>
/// A fluent <see langword="interface"/> to configure KickStart.
/// </summary>
public interface IConfigurationBuilder
{
    /// <summary>
    /// Include the current loaded assemblies as a type scaning source.
    /// </summary>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder IncludeLoadedAssemblies();

    /// <summary>
    /// Include the specified <see cref="Assembly"/> as a type scaning source.
    /// </summary>
    /// <param name="assembly">The assembly to include.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder IncludeAssembly(Assembly assembly);

    /// <summary>
    /// Include the assemblies that contain the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The name to compare.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder IncludeName(string name);


    /// <summary>
    /// Exclude the specified <see cref="Assembly"/> as a type scaning source.
    /// </summary>
    /// <param name="assembly">The assembly to exclude.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder ExcludeAssembly(Assembly assembly);

    /// <summary>
    /// Exclude the assemblies that start with the specified name as a type scaning source.
    /// </summary>
    /// <param name="name">The name to compare.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder ExcludeName(string name);


    /// <summary>
    /// Sets the context data element with the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key of the element to set.</param>
    /// <param name="value">The value for specified key.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder Data(string key, object value);

    /// <summary>
    /// Configure the context data with the specified <paramref name="data"/> <see langword="delegate"/>.
    /// </summary>
    /// <param name="data">The <see langword="delegate"/> to configure context data.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder Data(Action<IDictionary<string, object>> data);


    /// <summary>
    /// Run the specified IKickStarter extension on startup.
    /// </summary>
    /// <param name="starter">The IKickStarter extension to run.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder Use(IKickStarter starter);

    /// <summary>
    /// Set a <see langword="delegate"/> where log messages will be written.
    /// </summary>
    /// <param name="writer">The writer <see langword="delegate"/>..</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    IConfigurationBuilder LogTo(Action<string> writer);
}