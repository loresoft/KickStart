using Microsoft.Extensions.DependencyInjection;


namespace KickStart.DependencyInjection;

/// <summary>
/// Microsoft.Extensions.DependencyInjection configuration builder
/// </summary>
public class DependencyInjectionBuilder : IDependencyInjectionBuilder
{
    private readonly DependencyInjectionOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyInjectionBuilder"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public DependencyInjectionBuilder(DependencyInjectionOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Sets the service provider accessor <see langword="delegate" />. Resolve services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="accessor">The service provider accessor <see langword="delegate" />.</param>
    /// <returns></returns>
    public IDependencyInjectionBuilder Container(Action<IServiceProvider> accessor)
    {
        _options.Accessor = accessor;
        return this;
    }

    /// <summary>
    /// Sets the <see cref="IServiceCollection" /> creator <see langword="delegate" />.
    /// </summary>
    /// <param name="creator">The <see cref="IServiceCollection" /> creator.</param>
    /// <returns></returns>
    public IDependencyInjectionBuilder Creator(Func<IServiceCollection> creator)
    {
        _options.Creator = creator;
        return this;
    }

    /// <summary>
    /// Sets the initialize services <see langword="delegate" />.  Register services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="initializer">The initialize services <see langword="delegate" />.</param>
    /// <returns></returns>
    public IDependencyInjectionBuilder Initialize(Action<IServiceCollection> initializer)
    {
        _options.Initializer = initializer;
        return this;
    }
}
