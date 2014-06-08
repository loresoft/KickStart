using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace KickStart
{
    /// <summary>
    /// A base <see langword="class"/> that defines an application KickStart extension.
    /// </summary>
    public abstract class KickStarter : IKickStarter
    {
        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context"/> containing assemblies to scan.</param>
        public abstract void Run(Context context);

        /// <summary>
        /// Gets the instances assignable from the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The Type to scan for</typeparam>
        /// <param name="context">The KickStart <see cref="Context"/> containing assemblies to scan.</param>
        /// <param name="useContainer">if set to <c>true</c>, use <see cref="Kick.Container"/> to resolve instances.</param>
        /// <returns>An enumerable list of instances of type <typeparamref name="T"/>.</returns>
        protected virtual IEnumerable<T> GetInstancesAssignableFrom<T>(Context context, bool useContainer = false) 
            where T : class
        {
            if (useContainer && context.Container != null)
            {
                Logger.Verbose()
                    .Message("Resolve instances using Container: {0}", context.Container)
                    .Write();
                
                return context.Container.ResolveAll<T>().ToList();
            }

            return context.Assemblies
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
        protected virtual IEnumerable<Type> GetTypesAssignableFrom<T>(Assembly assembly)
        {
            Logger.Verbose()
                .Message("Scan Start; Assembly: '{0}', Type: '{1}'", assembly.FullName, typeof(T))
                .Write();

            Stopwatch watch = Stopwatch.StartNew();
            var types = assembly.GetTypesAssignableFrom<T>(); 
            watch.Stop();

            Logger.Verbose()
                .Message("Scan Complete; Assembly: '{0}', Type: '{1}', Time: {2} ms", assembly.FullName, typeof(T), watch.ElapsedMilliseconds)
                .Write();

            return types;
        }

        /// <summary>
        /// Create an instance of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to create an instance of.</param>
        /// <returns>An instance of the specified <paramref name="type"/>.</returns>
        protected virtual object CreateInstance(Type type)
        {
            Logger.Verbose()
                .Message("Create Instance: {0}", type)
                .Write();

            return Activator.CreateInstance(type);
        }
    }
}