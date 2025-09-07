using SimpleInjector;

namespace KickStart.SimpleInjector;

/// <summary>
/// SimpleInjector registration module
/// </summary>
public interface ISimpleInjectorRegistration
{
    /// <summary>
    /// Register injections with the specified container.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <param name="data">The data dictionary shared with all starter modules.</param>
    void Register(Container container, IDictionary<string, object> data);
}