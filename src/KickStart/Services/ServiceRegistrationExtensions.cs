namespace KickStart.Services;

/// <summary>
/// Extension methods for adding services to an <see cref="IServiceRegistration" />.
/// </summary>
public static class ServiceRegistrationExtensions
{
    /// <summary>
    /// Registers a transient service of the type specified in <paramref name="serviceType"/> with an
    /// implementation of the type specified in <paramref name="implementationType"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceRegistration RegisterTransient(this IServiceRegistration services, Type serviceType, Type implementationType)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        if (implementationType == null)
            throw new ArgumentNullException(nameof(implementationType));

        return services.Register(serviceType, implementationType, ServiceLifetime.Transient);
    }

    /// <summary>
    /// Registers a transient service of the type specified in <paramref name="serviceType"/> with a
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceRegistration RegisterTransient(this IServiceRegistration services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.Register(serviceType, implementationFactory, ServiceLifetime.Transient);
    }

    /// <summary>
    /// Registers a transient service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceRegistration RegisterTransient<TService, TImplementation>(this IServiceRegistration services)
        where TService : class
        where TImplementation : class, TService
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        return services.RegisterTransient(typeof(TService), typeof(TImplementation));
    }

    /// <summary>
    /// Registers a transient service of the type specified in <paramref name="serviceType"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceRegistration RegisterTransient(this IServiceRegistration services, Type serviceType)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        return services.RegisterTransient(serviceType, serviceType);
    }

    /// <summary>
    /// Registers a transient service of the type specified in <typeparamref name="TService"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceRegistration RegisterTransient<TService>(this IServiceRegistration services)
        where TService : class
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        return services.RegisterTransient(typeof(TService));
    }

    /// <summary>
    /// Registers a transient service of the type specified in <typeparamref name="TService"/> with a
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceRegistration RegisterTransient<TService>(this IServiceRegistration services, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.RegisterTransient(typeof(TService), implementationFactory);
    }

    /// <summary>
    /// Registers a transient service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation" /> using the
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceRegistration RegisterTransient<TService, TImplementation>(this IServiceRegistration services, Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.RegisterTransient(typeof(TService), implementationFactory);
    }

    /// <summary>
    /// Scan and Register transient services using the specified builder.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="builder">The builder delegate used to scan and register services</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceRegistration RegisterTransient(this IServiceRegistration services, Action<IServiceRegistrationBuilder> builder)
    {
        return services.Register(builder, ServiceLifetime.Transient);
    }


    /// <summary>
    /// Registers a scoped service of the type specified in <paramref name="serviceType"/> with an
    /// implementation of the type specified in <paramref name="implementationType"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceRegistration RegisterScoped(this IServiceRegistration services, Type serviceType, Type implementationType)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        if (implementationType == null)
            throw new ArgumentNullException(nameof(implementationType));

        return services.Register(serviceType, implementationType, ServiceLifetime.Scoped);
    }

    /// <summary>
    /// Registers a scoped service of the type specified in <paramref name="serviceType"/> with a
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceRegistration RegisterScoped(this IServiceRegistration services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.Register(serviceType, implementationFactory, ServiceLifetime.Scoped);
    }

    /// <summary>
    /// Registers a scoped service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceRegistration RegisterScoped<TService, TImplementation>(this IServiceRegistration services)
        where TService : class
        where TImplementation : class, TService
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        return services.RegisterScoped(typeof(TService), typeof(TImplementation));
    }

    /// <summary>
    /// Registers a scoped service of the type specified in <paramref name="serviceType"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceRegistration RegisterScoped(this IServiceRegistration services, Type serviceType)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        return services.RegisterScoped(serviceType, serviceType);
    }

    /// <summary>
    /// Registers a scoped service of the type specified in <typeparamref name="TService"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceRegistration RegisterScoped<TService>(this IServiceRegistration services)
        where TService : class
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        return services.RegisterScoped(typeof(TService));
    }

    /// <summary>
    /// Registers a scoped service of the type specified in <typeparamref name="TService"/> with a
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceRegistration RegisterScoped<TService>(this IServiceRegistration services, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.RegisterScoped(typeof(TService), implementationFactory);
    }

    /// <summary>
    /// Registers a scoped service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation" /> using the
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceRegistration RegisterScoped<TService, TImplementation>(this IServiceRegistration services, Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.RegisterScoped(typeof(TService), implementationFactory);
    }

    /// <summary>
    /// Scan and Register scoped services using the specified builder.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="builder">The builder delegate used to scan and register services</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceRegistration RegisterScoped(this IServiceRegistration services, Action<IServiceRegistrationBuilder> builder)
    {
        return services.Register(builder, ServiceLifetime.Scoped);
    }


    /// <summary>
    /// Registers a singleton service of the type specified in <paramref name="serviceType"/> with an
    /// implementation of the type specified in <paramref name="implementationType"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton(this IServiceRegistration services, Type serviceType, Type implementationType)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        if (implementationType == null)
            throw new ArgumentNullException(nameof(implementationType));

        return services.Register(serviceType, implementationType, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <paramref name="serviceType"/> with a
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton(this IServiceRegistration services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.Register(serviceType, implementationFactory, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton<TService, TImplementation>(this IServiceRegistration services)
        where TService : class
        where TImplementation : class, TService
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        return services.RegisterSingleton(typeof(TService), typeof(TImplementation));
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <paramref name="serviceType"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton(this IServiceRegistration services, Type serviceType)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        return services.RegisterSingleton(serviceType, serviceType);
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <typeparamref name="TService"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton<TService>(this IServiceRegistration services)
        where TService : class
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        return services.RegisterSingleton(typeof(TService));
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <typeparamref name="TService"/> with a
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton<TService>(this IServiceRegistration services, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.RegisterSingleton(typeof(TService), implementationFactory);
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <typeparamref name="TService"/> with an
    /// implementation type specified in <typeparamref name="TImplementation" /> using the
    /// factory specified in <paramref name="implementationFactory"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton<TService, TImplementation>(this IServiceRegistration services, Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (implementationFactory == null)
            throw new ArgumentNullException(nameof(implementationFactory));

        return services.RegisterSingleton(typeof(TService), implementationFactory);
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <paramref name="serviceType"/> with an
    /// instance specified in <paramref name="implementationInstance"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationInstance">The instance of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton(this IServiceRegistration services, Type serviceType, object implementationInstance)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (serviceType == null)
            throw new ArgumentNullException(nameof(serviceType));

        if (implementationInstance == null)
            throw new ArgumentNullException(nameof(implementationInstance));

        return services.Register(serviceType, p => implementationInstance, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Registers a singleton service of the type specified in <typeparamref name="TService" /> with an
    /// instance specified in <paramref name="implementationInstance"/> to the
    /// specified <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="implementationInstance">The instance of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceRegistration RegisterSingleton<TService>(this IServiceRegistration services, TService implementationInstance)
        where TService : class
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (implementationInstance == null)
            throw new ArgumentNullException(nameof(implementationInstance));

        return services.RegisterSingleton(typeof(TService), implementationInstance);
    }

    /// <summary>
    /// Scan and Register singleton services using the specified <paramref name="builder"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="builder">The builder delegate used to scan and register services</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceRegistration RegisterSingleton(this IServiceRegistration services, Action<IServiceRegistrationBuilder> builder)
    {
        return services.Register(builder, ServiceLifetime.Singleton);
    }


    /// <summary>
    /// Scan and Register services using the specified <paramref name="builder"/> for the specified <paramref name="lifetime"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceRegistration"/> to add the service to.</param>
    /// <param name="builder">The builder delegate used to scan and register services</param>
    /// <param name="lifetime">The service lifetime.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceRegistration Register(this IServiceRegistration services, Action<IServiceRegistrationBuilder> builder, ServiceLifetime lifetime)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        var types = services.ServiceContext.Types;

        var serviceBuilder = new ServiceRegistrationBuilder(types);
        builder(serviceBuilder);

        var mapping = serviceBuilder.TypeMaps;
        foreach (var typeMap in mapping)
            foreach (var serviceType in typeMap.ServiceTypes)
                services.Register(serviceType, typeMap.ImplementationType, lifetime);

        return services;
    }
}