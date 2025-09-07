using SimpleInjector;

namespace KickStart.SimpleInjector;

/// <summary>
/// SimpleInjector options class
/// </summary>
public class SimpleInjectorOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleInjectorOptions"/> class.
    /// </summary>
    public SimpleInjectorOptions()
    {
        Creator = () => new Container();
    }

    /// <summary>
    /// Gets or sets the container accessor <see langword="delegate" />.
    /// </summary>
    /// <value>
    /// The container accessor <see langword="delegate" />.
    /// </value>
    public Action<Container> Accessor { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Container" /> creator <see langword="delegate" />.
    /// </summary>
    /// <value>
    /// The <see cref="Container" /> creator <see langword="delegate" />.
    /// </value>
    public Func<Container> Creator { get; set; }

    /// <summary>
    /// Gets or sets the initialize container <see langword="delegate" />.
    /// </summary>
    /// <value>
    /// The initialize container <see langword="delegate" />.
    /// </value>
    public Action<Container> Initializer { get; set; }

    /// <summary>
    /// Gets or sets how the container should verify its configuration.
    /// </summary>
    /// <value>
    /// How the container should verify its configuration. 
    /// </value>
    public VerificationOption? VerificationOption { get; set; }
}
