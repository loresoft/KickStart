using KickStart.Unity;

// ReSharper disable once CheckNamespace
namespace KickStart;

/// <summary>
/// KickStart Unity extension methods
/// </summary>
public static class UnityExtensions
{
    /// <summary>
    /// Use the KickStart extension to configure Unity.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <returns>
    /// A fluent <see langword="interface" /> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder UseUnity(this IConfigurationBuilder configurationBuilder)
    {
        return UseUnity(configurationBuilder, null);
    }

    /// <summary>
    /// Use the KickStart extension to configure Unity.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <param name="configure">The <see langword="delegate"/> to configure Unity options.</param>
    /// <returns>
    /// A fluent <see langword="interface" /> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder UseUnity(this IConfigurationBuilder configurationBuilder, Action<IUnityBuilder> configure)
    {
        var options = new UnityOptions();
        var starter = new UnityStarter(options);

        if (configure != null)
        {
            var builder = new UnityBuilder(options);
            configure(builder);
        }

        configurationBuilder.ExcludeAssemblyFor<UnityStarter>();
        configurationBuilder.ExcludeAssemblyFor<global::Unity.IUnityContainer>();
        configurationBuilder.Use(starter);

        return configurationBuilder;
    }
}