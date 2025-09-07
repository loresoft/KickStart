using SimpleInjector;

namespace KickStart.SimpleInjector;

/// <summary>
/// SimpleInjector configuration builder
/// </summary>
public class SimpleInjectorBuilder : ISimpleInjectorBuilder
{
    private readonly SimpleInjectorOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleInjectorBuilder"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public SimpleInjectorBuilder(SimpleInjectorOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Sets the container accessor <see langword="delegate" />.  Resolve services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="accessor">The container accessor <see langword="delegate" />.</param>
    /// <returns></returns>
    public ISimpleInjectorBuilder Container(Action<Container> accessor)
    {
        _options.Accessor = accessor;
        return this;
    }

    /// <summary>
    /// Sets the <see cref="Container" /> creator <see langword="delegate" />.
    /// </summary>
    /// <param name="creator">The <see cref="Container" /> creator.</param>
    /// <returns></returns>
    public ISimpleInjectorBuilder Creator(Func<Container> creator)
    {
        _options.Creator = creator;
        return this;
    }

    /// <summary>
    /// Sets the initialize container <see langword="delegate" />.  Register services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="initializer">The initialize container <see langword="delegate" />.</param>
    /// <returns></returns>
    public ISimpleInjectorBuilder Initialize(Action<Container> initializer)
    {
        _options.Initializer = initializer;
        return this;
    }

    /// <summary>
    /// Verifies the Container with the specified <paramref name="options" />.
    /// </summary>
    /// <param name="options">Specifies how the container should verify its configuration.</param>
    /// <returns></returns>
    public ISimpleInjectorBuilder Verify(VerificationOption? options)
    {
        _options.VerificationOption = options;
        return this;
    }
}