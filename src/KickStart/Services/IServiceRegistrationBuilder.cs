namespace KickStart.Services;

/// <summary>
/// An <see langword="interface"/> to build service registrations
/// </summary>
public interface IServiceRegistrationBuilder
{
    /// <summary>
    /// Filters the types based on a <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>An <see langword="interface"/> to build service registrations</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> argument is <c>null</c>.</exception>
    IServiceRegistrationBuilder Types(Action<IConcreteTypeFilter> predicate);

    /// <summary>
    /// Configure how the types should be registered as.
    /// </summary>
    /// <param name="mapper">A function to configure how types should be registered.</param>
    /// <returns>An <see langword="interface"/> to build service registrations</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="mapper"/> argument is <c>null</c>.</exception>
    IServiceRegistrationBuilder As(Action<IServiceTypeMapper> mapper);
}