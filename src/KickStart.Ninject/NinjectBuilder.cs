using Ninject;

namespace KickStart.Ninject;

/// <summary>
/// Ninject configuration builder
/// </summary>
/// <seealso cref="KickStart.Ninject.INinjectBuilder" />
public class NinjectBuilder : INinjectBuilder
{
    private readonly NinjectOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="NinjectBuilder"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public NinjectBuilder(NinjectOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Set the specified Ninject settings.
    /// </summary>
    /// <param name="settings">The Ninjectsettings.</param>
    /// <returns></returns>
    public INinjectBuilder Settings(INinjectSettings settings)
    {
        _options.Settings = settings;
        return this;
    }

    /// <summary>
    /// Set the specified <see cref="IKernel" /> initializer.
    /// </summary>
    /// <param name="initializer">The <see cref="IKernel" /> initializer.</param>
    /// <returns></returns>
    public INinjectBuilder Kernal(Action<IKernel> initializer)
    {
        _options.InitializeKernel = initializer;
        return this;
    }

}
