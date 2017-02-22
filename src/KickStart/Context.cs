using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using KickStart.Logging;
using KickStart.Services;
#if PORTABLE
using Stopwatch = KickStart.Portability.Stopwatch;
#else
using Stopwatch = System.Diagnostics.Stopwatch;
#endif

namespace KickStart
{
    /// <summary>
    /// The KickStart running context.
    /// </summary>
    public class Context
    {
        private static readonly ILogger _logger = Logger.CreateLogger<Context>();
        private readonly ReadOnlyCollection<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public Context(IEnumerable<Assembly> assemblies)
        {
            _assemblies = new ReadOnlyCollection<Assembly>(assemblies.ToList());
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IServiceProvider ServiceProvider
        {
            get { return Kick.ServiceProvider; }
        }

        /// <summary>
        /// Gets the assemblies used by KickStart.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        public ReadOnlyCollection<Assembly> Assemblies
        {
            get { return _assemblies; }
        }


        /// <summary>
        /// Sets the global <see cref="P:KickStart.Kick.Container"/>.
        /// </summary>
        /// <param name="serviceProvider">The container adaptor to assign.</param>
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _logger.Trace()
                .Message("Assign Kick service provider: {0}", serviceProvider)
                .Write();

            Kick.SetServiceProvider(serviceProvider);
        }


        /// <summary>
        /// Gets the instances assignable from the specified generic type.
        /// </summary>
        /// <typeparam name="T">The Type to scan for</typeparam>
        /// <param name="useContainer">if set to <c>true</c>, use <see cref="Kick.ServiceProvider"/> to resolve instances.</param>
        /// <returns>An enumerable list of instances of type <typeparamref name="T"/>.</returns>
        public virtual IEnumerable<T> GetInstancesAssignableFrom<T>(bool useContainer = false)
            where T : class
        {
            if (!useContainer || ServiceProvider == null)
                return Assemblies
                    .SelectMany(GetTypesAssignableFrom<T>)
                    .Select(CreateInstance)
                    .OfType<T>()
                    .ToList();


            _logger.Trace()
                .Message("Resolve instances using Container: {0}", ServiceProvider)
                .Write();

            return ServiceProvider.GetServices<T>().ToList();
        }

        /// <summary>
        /// Gets the types assignable from type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to determine whether if it can be assigned.</typeparam>
        /// <param name="assembly">The assembly to search types.</param>
        /// <returns>An enumerable list of types the are assignable from <typeparamref name="T"/>.</returns>
        public virtual IEnumerable<Type> GetTypesAssignableFrom<T>(Assembly assembly)
        {
            _logger.Trace()
                .Message("Scan Start; Assembly: '{0}', Type: '{1}'", assembly.FullName, typeof(T))
                .Write();

            var watch = Stopwatch.StartNew();
            var types = assembly.GetTypesAssignableFrom<T>();
            watch.Stop();

            _logger.Trace()
                .Message("Scan Complete; Assembly: '{0}', Type: '{1}', Time: {2} ms", assembly.FullName, typeof(T), watch.ElapsedMilliseconds)
                .Write();

            return types;
        }

        /// <summary>
        /// Create an instance of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to create an instance of.</param>
        /// <returns>An instance of the specified <paramref name="type"/>.</returns>
        public virtual object CreateInstance(Type type)
        {
            _logger.Trace()
                .Message("Create Instance: {0}", type)
                .Write();

            return Activator.CreateInstance(type);
        }

    }
}