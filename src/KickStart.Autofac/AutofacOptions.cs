using Autofac;
using Autofac.Builder;

namespace KickStart.Autofac;

/// <summary>
/// 
/// </summary>
public class AutofacOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutofacOptions"/> class.
    /// </summary>
    public AutofacOptions()
    {
        BuildOptions = ContainerBuildOptions.None;
    }

    /// <summary>
    /// Gets or sets the build options.
    /// </summary>
    /// <value>
    /// The build options.
    /// </value>
    public ContainerBuildOptions BuildOptions { get; set; }

    /// <summary>
    /// Gets or sets the initialize builder <see langword="delegate"/>.
    /// </summary>
    /// <value>
    /// The initialize builder <see langword="delegate"/>.
    /// </value>
    public Action<ContainerBuilder> Initializer { get; set; }

    /// <summary>
    /// Gets or sets the initialize container <see langword="delegate"/>.
    /// </summary>
    /// <value>
    /// The initialize container <see langword="delegate"/>.
    /// </value>
    public Action<IContainer> Accessor { get; set; }
}