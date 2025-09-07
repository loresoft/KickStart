namespace KickStart.StartupTask;

/// <summary>
/// A fluent <see langword="class"/> to configure startup tasks
/// </summary>
public class StartupTaskBuilder : IStartupTaskBuilder
{
    private readonly StartupTaskOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartupTaskBuilder"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public StartupTaskBuilder(StartupTaskOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Runs the specified startup action.
    /// </summary>
    /// <param name="startupAction">The startup action.</param>
    /// <returns>
    /// A fluent <see langword="interface" /> to configure startup tasks
    /// </returns>
    public IStartupTaskBuilder Run(Action<IServiceProvider, IDictionary<string, object>> startupAction)
    {
        _options.Actions.Add(startupAction);
        return this;
    }
}