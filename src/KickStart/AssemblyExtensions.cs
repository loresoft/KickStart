using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KickStart
{
    /// <summary>
    ///   <see cref="Assembly" /> extension methods
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the public types defined in this assembly that are visible and can be loaded outside the assembly.
        /// </summary>
        /// <param name="assembly">The assembly to search types.</param>
        /// <returns>The types defined in this assembly that are visible outside the assembly.</returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            // skip dynamic assemblies
            if (assembly.IsDynamic)
                return Enumerable.Empty<Type>();

            Type[] types;

            try
            {
                types = assembly.GetExportedTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                //not interested in the types which cause the problem, load what we can
                types = e.Types.Where(t => t != null).ToArray();
            }
            catch (NotSupportedException)
            {
                // some assemblies don't support getting types, ignore
                return Enumerable.Empty<Type>();
            }

            return types;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified <paramref name="otherType"/> can be assigned to the current <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The current type.</param>
        /// <param name="otherType">The type to check.</param>
        /// <returns>true if the specified type can be assigned to this type; otherwise, false.</returns>
        public static bool IsAssignableTo(this Type type, Type otherType)
        {
            var typeInfo = type.GetTypeInfo();
            var otherTypeInfo = otherType.GetTypeInfo();

            return otherTypeInfo.IsAssignableFrom(typeInfo);
        }

        /// <summary>
        /// Returns a value that indicates whether the specified <paramref name="type"/> is concrete.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>true if the specified type is concrete; otherwise, false.</returns>
        public static bool IsConcreteType(this Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return typeInfo.IsClass && !typeInfo.IsAbstract;
        }

#if NET40 || PORTABLE
        /// <summary>
        /// Retrieves an object that represents this type.
        /// </summary>
        /// <param name="type">Type to retrieve</param>
        /// <returns></returns>
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }
#endif
    }
}