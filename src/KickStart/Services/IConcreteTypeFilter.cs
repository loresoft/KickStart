namespace KickStart.Services;

/// <summary>
/// An <see langword="interface"/> to filter service types
/// </summary>
public interface IConcreteTypeFilter
{
    /// <summary>
    /// Filter to types that are assignable to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type that should be assignable.</typeparam>
    /// <returns>An <see langword="interface"/> to filter service types</returns>
    IConcreteTypeFilter AssignableTo<T>();

    /// <summary>
    /// Filter to types that are assignable to the specified <paramref name="type" />.
    /// </summary>
    /// <param name="type">The type that should be assignable.</param>
    /// <returns>An <see langword="interface"/> to filter service types</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="type"/> argument is <c>null</c>.</exception>
    IConcreteTypeFilter AssignableTo(Type type);

    /// <summary>
    /// Filter to types that are assignable to any of the specified <paramref name="types" />.
    /// </summary>
    /// <param name="types">>The types that should be assignable.</param>
    /// <returns>An <see langword="interface"/> to filter service types</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
    IConcreteTypeFilter AssignableToAny(params Type[] types);

    /// <summary>
    /// Filter to types that are assignable to any of the specified <paramref name="types" />.
    /// </summary>
    /// <param name="types">>The types that should be assignable.</param>
    /// <returns>An <see langword="interface"/> to filter service types</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
    IConcreteTypeFilter AssignableToAny(IEnumerable<Type> types);

    /// <summary>
    /// Filter types based on the specified <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">The predicate to filter types.</param>
    /// <returns>An <see langword="interface"/> to filter service types</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="predicate" /> argument is <c>null</c>.</exception>
    IConcreteTypeFilter Where(Func<Type, bool> predicate);
}