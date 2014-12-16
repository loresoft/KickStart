using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace KickStart
{
    /// <summary>
    /// The KickStart running context.
    /// </summary>
    public class Context
    {
        private readonly IReadOnlyList<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public Context(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IContainerAdaptor Container
        {
            get { return Kick.Container; }
        }

        /// <summary>
        /// Gets the assemblies used by KickStart.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        public IReadOnlyList<Assembly> Assemblies
        {
            get { return _assemblies; }
        }


        /// <summary>
        /// Sets the global <see cref="P:KickStart.Kick.Container"/>.
        /// </summary>
        /// <param name="container">The container adaptor to assign.</param>
        public void SetContainer(IContainerAdaptor container)
        {
            Logger.Trace()
               .Message("Assign Kick Container: {0}", container)
               .Write();

            Kick.SetContainer(container);
        }


        /// <summary>
        /// Gets the instances assignable from the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The Type to scan for</typeparam>
        /// <param name="useContainer">if set to <c>true</c>, use <see cref="Kick.Container"/> to resolve instances.</param>
        /// <returns>An enumerable list of instances of type <typeparamref name="T"/>.</returns>
        public virtual IEnumerable<T> GetInstancesAssignableFrom<T>(bool useContainer = false)
            where T : class
        {
            if (useContainer && Container != null)
            {
                Logger.Trace()
                    .Message("Resolve instances using Container: {0}", Container)
                    .Write();

                return Container.ResolveAll<T>().ToList();
            }

            return Assemblies
                .SelectMany(GetTypesAssignableFrom<T>)
                .Select(CreateInstance)
                .OfType<T>()
                .ToList();
        }

        /// <summary>
        /// Gets the types assignable from type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to determine whether if it can be assigned.</typeparam>
        /// <param name="assembly">The assembly to search types.</param>
        /// <returns>An enumerable list of types the are assignable from <typeparamref name="T"/>.</returns>
        public virtual IEnumerable<Type> GetTypesAssignableFrom<T>(Assembly assembly)
        {
            Logger.Trace()
                .Message("Scan Start; Assembly: '{0}', Type: '{1}'", assembly.FullName, typeof(T))
                .Write();

            Stopwatch watch = Stopwatch.StartNew();
            var types = assembly.GetTypesAssignableFrom<T>();
            watch.Stop();

            Logger.Trace()
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
            Logger.Trace()
                .Message("Create Instance: {0}", type)
                .Write();

            return Activator.CreateInstance(type);
        }

    }
}