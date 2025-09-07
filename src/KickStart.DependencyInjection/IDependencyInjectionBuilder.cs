using Microsoft.Extensions.DependencyInjection;


namespace KickStart.DependencyInjection;

/// <summary>
/// Microsoft.Extensions.DependencyInjection configuration builder
/// </summary>
public interface IDependencyInjectionBuilder
{
    /// <summary>
    /// Sets the <see cref="IServiceCollection"/> creator <see langword="delegate" />.
    /// </summary>
    /// <param name="creator">The <see cref="IServiceCollection"/> creator.</param>
    /// <returns></returns>
    IDependencyInjectionBuilder Creator(Func<IServiceCollection> creator);


    /// <summary>
    /// Sets the service provider accessor <see langword="delegate" />. Resolve services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="accessor">The service provider accessor <see langword="delegate"/>.</param>
    /// <returns></returns>
    IDependencyInjectionBuilder Container(Action<IServiceProvider> accessor);

    /// <summary>
    /// Sets the initialize services <see langword="delegate" />.  Register services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="initializer">The initialize services <see langword="delegate"/>.</param>
    /// <returns></returns>
    IDependencyInjectionBuilder Initialize(Action<IServiceCollection> initializer);
}
