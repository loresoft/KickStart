using Unity;

namespace KickStart.Unity;

/// <summary>
/// Unity fluent builder
/// </summary>
/// <seealso cref="KickStart.Unity.IUnityBuilder" />
public class UnityBuilder : IUnityBuilder
{
    private readonly UnityOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnityBuilder"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public UnityBuilder(UnityOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Sets the initialize container <see langword="delegate" />.
    /// </summary>
    /// <param name="initializer">The initializer the container.</param>
    /// <returns></returns>
    public IUnityBuilder Container(Action<IUnityContainer> initializer)
    {
        _options.InitializeContainer = initializer;
        return this;
    }
}