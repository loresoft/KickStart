namespace KickStart.StartupTask;

/// <summary>
/// Startup tasks options class
/// </summary>
public class StartupTaskOptions
{
    /// <summary>
    /// Gets the startup actions.
    /// </summary>
    /// <value>
    /// The startup actions.
    /// </value>
    public IList<Action<IServiceProvider, IDictionary<string, object>>> Actions { get; } = new List<Action<IServiceProvider, IDictionary<string, object>>>();
}