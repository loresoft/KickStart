using System.Collections.Concurrent;

namespace KickStart;

/// <summary>
/// A class defining the KickStart configuration.
/// </summary>
public class Configuration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Configuration"/> class.
    /// </summary>
    public Configuration()
    {
        Assemblies = new AssemblyResolver(m => LogWriter?.Invoke(m));

        // exclude system assemblies
        Assemblies.ExcludeName("mscorlib");
        Assemblies.ExcludeName("Microsoft");
        Assemblies.ExcludeName("System");

        // exclude self
        Assemblies.ExcludeAssemblyFor<Configuration>();

        Starters = new List<IKickStarter>();
        Data = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets the assemblies used by KickStart.
    /// </summary>
    /// <value>
    /// The assemblies use by KickStart.
    /// </value>
    public AssemblyResolver Assemblies { get; }

    /// <summary>
    /// Gets the <see cref="IKickStarter"/> extensions to use.
    /// </summary>
    /// <value>
    /// The IKickStarter extensions to use.
    /// </value>
    public IList<IKickStarter> Starters { get; }

    /// <summary>
    /// Gets the data dictionary shared with all starter modules.
    /// </summary>
    /// <value>
    /// The data dictionary shared with all starter modules.
    /// </value>
    public IDictionary<string, object> Data { get; }

    /// <summary>
    /// Gets or set the <see langword="delegate" /> where log messages will be written.
    /// </summary>
    /// <value>
    /// The <see langword="delegate" /> where log messages will be written.
    /// </value>
    public Action<string> LogWriter { get; set; }
}