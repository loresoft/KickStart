using KickStart.AutoMapper;

// ReSharper disable once CheckNamespace
namespace KickStart;

/// <summary>
/// KickStart Extension for AutoMapper.
/// </summary>
public static class AutoMapperExtensions
{
    /// <summary>
    /// Use the KickStart extension to configure AutoMapper.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    /// <example>Configure AutoMapper on application startup
    /// <code><![CDATA[
    /// Kick.Start(config => config
    ///     .IncludeAssemblyFor<UserProfile>()
    ///     .UseAutoMapper()
    ///     .LogLevel(TraceLevel.Verbose)
    /// );]]></code>
    /// </example>
    public static IConfigurationBuilder UseAutoMapper(this IConfigurationBuilder configurationBuilder)
    {
        return UseAutoMapper(configurationBuilder, null);
    }

    /// <summary>
    /// Use the KickStart extension to configure AutoMapper.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <param name="configure">The <see langword="delegate"/> to configure AutoMapper options.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    /// <example>Configure AutoMapper on application startup
    /// <code><![CDATA[
    /// Kick.Start(config => config
    ///     .IncludeAssemblyFor<UserProfile>()
    ///     .UseAutoMapper(c => c
    ///         .Validate()
    ///         .Initialize(map => map.AddGlobalIgnore("SysVersion"))
    ///     )
    ///     .LogLevel(TraceLevel.Verbose)
    /// );]]></code>
    /// </example>
    public static IConfigurationBuilder UseAutoMapper(this IConfigurationBuilder configurationBuilder, Action<IAutoMapperBuilder> configure)
    {
        var options = new AutoMapperOptions();
        var service = new AutoMapperStarter(options);

        if (configure != null)
        {
            var builder = new AutoMapperBuilder(options);
            configure(builder);
        }

        configurationBuilder.ExcludeAssemblyFor<AutoMapperStarter>();
        configurationBuilder.ExcludeAssemblyFor<global::AutoMapper.Mapper>();
        configurationBuilder.Use(service);

        return configurationBuilder;
    }

}