using Autofac;
using Autofac.Builder;

namespace KickStart.Autofac;

/// <summary>
/// Autofac builder interface
/// </summary>
public interface IAutofacBuilder
{
    /// <summary>
    /// Sets Autofac build options.
    /// </summary>
    /// <param name="buildOptions">The build options.</param>
    /// <returns></returns>
    IAutofacBuilder Options(ContainerBuildOptions buildOptions);

    /// <summary>
    /// Sets the initialize builder <see langword="delegate" />.  Register services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="initializer">The initialize builder <see langword="delegate"/>.</param>
    /// <returns></returns>
    IAutofacBuilder Initialize(Action<ContainerBuilder> initializer);

    /// <summary>
    /// Sets the container accessor <see langword="delegate" />.  Resolve services under this <see langword="delegate" />.
    /// </summary>
    /// <param name="accessor">The container accessor <see langword="delegate"/>.</param>
    /// <returns></returns>
    IAutofacBuilder Container(Action<IContainer> accessor);
}