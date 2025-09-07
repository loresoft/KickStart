namespace KickStart.Services;

/// <summary>
/// An <see langword="interface"/> to configure how service mappings are registered.
/// </summary>
public interface IServiceTypeMapper
{
    /// <summary>
    /// Registers each concrete type as itself.
    /// </summary>
    /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
    IServiceTypeMapper Self();

    /// <summary>
    /// Registers each concrete type as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to register as.</typeparam>
    /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
    IServiceTypeMapper Type<T>();

    /// <summary>
    /// Registers each concrete type as each of the specified <paramref name="types" />.
    /// </summary>
    /// <param name="types">The types to register as.</param>
    /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
    IServiceTypeMapper Types(params Type[] types);

    /// <summary>
    /// Registers each concrete type as each of the specified <paramref name="types" />.
    /// </summary>
    /// <param name="types">The types to register as.</param>
    /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
    IServiceTypeMapper Types(IEnumerable<Type> types);

    /// <summary>
    /// Registers each concrete type as all of its implemented interfaces.
    /// </summary>
    /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
    IServiceTypeMapper ImplementedInterfaces();
}