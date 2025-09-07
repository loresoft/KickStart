using Ninject;

namespace KickStart.Ninject;

/// <summary>
/// Ninject fluent builder
/// </summary>
public interface INinjectBuilder
{
    /// <summary>
    /// Set the specified Ninject settings.
    /// </summary>
    /// <param name="settings">The Ninjectsettings.</param>
    /// <returns></returns>
    INinjectBuilder Settings(INinjectSettings settings);
    /// <summary>
    /// Set the specified <see cref="IKernel"/> initializer.
    /// </summary>
    /// <param name="initializer">The <see cref="IKernel"/> initializer.</param>
    /// <returns></returns>
    INinjectBuilder Kernal(Action<IKernel> initializer);
}