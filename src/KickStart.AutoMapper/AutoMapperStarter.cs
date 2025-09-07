using AutoMapper;

namespace KickStart.AutoMapper;

/// <summary>
/// A KickStart extension to initialize AutoMapper.
/// </summary>
public class AutoMapperStarter : IKickStarter
{
    /// <summary>The AutoMapper configuration data key name</summary>
    public const string AutoMapperConfiguration = "AutoMapper:Configuration";

    private readonly AutoMapperOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperStarter"/> class.
    /// </summary>
    /// <param name="options">The options to use.</param>
    public AutoMapperStarter(AutoMapperOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Runs the application KickStart extension with specified <paramref name="context" />.
    /// </summary>
    /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
    public void Run(Context context)
    {
        var profiles = context.GetInstancesAssignableFrom<Profile>();

        var configuration = new MapperConfiguration(config =>
        {
            foreach (var profile in profiles)
            {
                context.WriteLog("AutoMapper Profile: {0}", profile);

                config.AddProfile(profile);
            }

            _options.Initialize?.Invoke(config);
        });


        if (_options.Validate)
            configuration.AssertConfigurationIsValid();

        context.Data[AutoMapperConfiguration] = configuration;
    }
}
