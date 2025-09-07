using KickStart.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;


// ReSharper disable once CheckNamespace
namespace KickStart;

/// <summary>
/// KickStart Extension for Microsoft.Extensions.DependencyInjection.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Use the KickStart extension to configure Microsoft.Extensions.DependencyInjection.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder UseDependencyInjection(this IConfigurationBuilder configurationBuilder)
    {
        return UseDependencyInjection(configurationBuilder, c => c.Creator(() => new ServiceCollection()));
    }

    /// <summary>
    /// Use the KickStart extension to configure Microsoft.Extensions.DependencyInjection.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <param name="collection">The initial service collection.</param>
    /// <returns>
    /// A fluent <see langword="interface" /> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder UseDependencyInjection(this IConfigurationBuilder configurationBuilder, IServiceCollection collection)
    {
        return UseDependencyInjection(configurationBuilder, c => c.Creator(() => collection));
    }

    /// <summary>
    /// Use the KickStart extension to configure Microsoft.Extensions.DependencyInjection.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <param name="configure">The <see langword="delegate"/> to configure DependencyInjection options.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder UseDependencyInjection(this IConfigurationBuilder configurationBuilder, Action<IDependencyInjectionBuilder> configure)
    {
        var options = new DependencyInjectionOptions();
        var starter = new DependencyInjectionStarter(options);

        if (configure != null)
        {
            var builder = new DependencyInjectionBuilder(options);
            configure(builder);
        }

        configurationBuilder.ExcludeAssemblyFor<DependencyInjectionStarter>();
        configurationBuilder.ExcludeAssemblyFor<IServiceCollection>();
        configurationBuilder.Use(starter);

        return configurationBuilder;
    }
}
