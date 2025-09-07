using KickStart.EntityChange;

// ReSharper disable once CheckNamespace
namespace KickStart;

/// <summary>
/// KickStart Extension for EntityChange.
/// </summary>
public static class EntityChangeExtensions
{
    /// <summary>
    /// Use the KickStart extension to configure EntityChange.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    /// <example>Configure EntityChange on application startup
    /// <code><![CDATA[
    /// Kick.Start(config => config
    ///     .IncludeAssemblyFor<UserProfile>()
    ///     .UseEntityChange()
    ///     .LogLevel(TraceLevel.Verbose)
    /// );]]></code>
    /// </example>
    public static IConfigurationBuilder UseEntityChange(this IConfigurationBuilder configurationBuilder)
    {
        return UseEntityChange(configurationBuilder, null);
    }

    /// <summary>
    /// Use the KickStart extension to configure EntityChange.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <param name="configure">The <see langword="delegate"/> to configure EntityChange options.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    /// <example>Configure EntityChange on application startup
    /// <code><![CDATA[
    /// Kick.Start(config => config
    ///     .IncludeAssemblyFor<UserProfile>()
    ///     .UseEntityChange(c => c
    ///         .Configure(config =>
    ///         {
    ///             config.Entity<Order>(e =>
    ///             {
    ///                 e.Property(p => p.Total).Formatter(StringFormatter.Currency);
    ///             });
    ///         });
    ///     )
    ///     .LogLevel(TraceLevel.Verbose)
    /// );]]></code>
    /// </example>
    public static IConfigurationBuilder UseEntityChange(this IConfigurationBuilder configurationBuilder, Action<IEntityChangeBuilder> configure)
    {
        var options = new EntityChangeOptions();
        var service = new EntityChangeStarter(options);

        if (configure != null)
        {
            var builder = new EntityChangeBuilder(options);
            configure(builder);
        }

        configurationBuilder.ExcludeAssemblyFor<EntityChangeStarter>();
        configurationBuilder.ExcludeAssemblyFor<global::EntityChange.EntityComparer>();
        configurationBuilder.Use(service);

        return configurationBuilder;
    }

}