using System.Reflection;

namespace KickStart;

/// <summary>
/// Extension methods for <see cref="IConfigurationBuilder"/>
/// </summary>
public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Include the specified <paramref name="assemblies"/> as a type scaning sources.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <param name="assemblies">The assemblies to include.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder IncludeAssemblies(this IConfigurationBuilder builder, IEnumerable<Assembly> assemblies)
    {
        if (assemblies == null)
            throw new ArgumentNullException(nameof(assemblies));

        foreach (var assembly in assemblies)
            builder.IncludeAssembly(assembly);

        return builder;
    }

    /// <summary>
    /// Include the assembly from the specified type <typeparamref name="T"/> as a type scaning source.
    /// </summary>
    /// <typeparam name="T">The type to get assembly from.</typeparam>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder IncludeAssemblyFor<T>(this IConfigurationBuilder builder)
    {
        var assembly = typeof(T).GetTypeInfo().Assembly;
        return builder.IncludeAssembly(assembly);
    }

    /// <summary>
    /// Include the assemblies from the specified <paramref name="types"/> as a type scaning source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <param name="types">The types to get assemblies from.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder IncludeAssembliesFor(this IConfigurationBuilder builder, IEnumerable<Type> types)
    {
        if (types == null)
            throw new ArgumentNullException(nameof(types));

        var assemblies = types
            .Select(t => t.GetTypeInfo().Assembly)
            .Distinct();

        foreach (var assembly in assemblies)
            builder.IncludeAssembly(assembly);

        return builder;
    }

    /// <summary>
    /// Include the assemblies that contain the specified <paramref name="names"/> as a type scaning source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <param name="names">The names to compare.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder IncludeNames(this IConfigurationBuilder builder, IEnumerable<string> names)
    {
        if (names == null)
            throw new ArgumentNullException(nameof(names));

        foreach (var name in names)
            builder.IncludeName(name);

        return builder;
    }


    /// <summary>
    /// Exclude the specified <paramref name="assemblies"/> as a type scaning sources.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <param name="assemblies">The assemblies to include.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder ExcludeAssemblies(this IConfigurationBuilder builder, IEnumerable<Assembly> assemblies)
    {
        if (assemblies == null)
            throw new ArgumentNullException(nameof(assemblies));

        foreach (var assembly in assemblies)
            builder.ExcludeAssembly(assembly);

        return builder;
    }

    /// <summary>
    /// Exclude the assembly from the specified type <typeparamref name="T"/> as a type scaning source.
    /// </summary>
    /// <typeparam name="T">The type to get assembly from.</typeparam>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder ExcludeAssemblyFor<T>(this IConfigurationBuilder builder)
    {
        var assembly = typeof(T).GetTypeInfo().Assembly;
        return builder.ExcludeAssembly(assembly);
    }

    /// <summary>
    /// Exclude the assemblies from the specified <paramref name="types"/> as a type scaning sources.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <param name="types">The types to get assemblies from.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder ExcludeAssembliesFor(this IConfigurationBuilder builder, IEnumerable<Type> types)
    {
        if (types == null)
            throw new ArgumentNullException(nameof(types));

        var assemblies = types
            .Select(t => t.GetTypeInfo().Assembly)
            .Distinct();

        foreach (var assembly in assemblies)
            builder.ExcludeAssembly(assembly);

        return builder;
    }

    /// <summary>
    /// Exclude the assemblies that contain the specified <paramref name="names"/> as a type scaning source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> to modify.</param>
    /// <param name="names">The names to compare.</param>
    /// <returns>
    /// A fluent <see langword="interface"/> to configure KickStart.
    /// </returns>
    public static IConfigurationBuilder ExcludeNames(this IConfigurationBuilder builder, IEnumerable<string> names)
    {
        if (names == null)
            throw new ArgumentNullException(nameof(names));

        foreach (var name in names)
            builder.ExcludeName(name);

        return builder;
    }
}