// ReSharper disable once CheckNamespace
namespace KickStart;

/// <summary>
/// KickStart Extension for NLog.
/// </summary>
public static class NLogExtensions
{
    private static NLog.Logger _logger = NLog.LogManager.GetLogger("KickStart");

    /// <summary>
    /// Use NLog as a logging target.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <returns></returns>
    public static IConfigurationBuilder UseNLog(this IConfigurationBuilder configurationBuilder)
    {
        return UseNLog(configurationBuilder, null);
    }


    /// <summary>
    /// Use NLog as a logging target.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder.</param>
    /// <param name="configure">The configure action for NLog.</param>
    /// <returns></returns>
    public static IConfigurationBuilder UseNLog(this IConfigurationBuilder configurationBuilder, Action<NLog.Config.LoggingConfiguration> configure)
    {
        if (configure != null)
        {
            var configuration = global::NLog.LogManager.Configuration ?? new NLog.Config.LoggingConfiguration();
            configure(configuration);

            // Activate the configuration
            NLog.LogManager.Configuration = configuration;
        }

        // register log writer
        configurationBuilder.LogTo(_logger.Debug);

        return configurationBuilder;
    }
}
