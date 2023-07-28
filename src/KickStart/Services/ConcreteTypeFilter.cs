using System;
using System.Collections.Generic;
using System.Linq;

namespace KickStart.Services
{
    /// <summary>
    /// Service type filter builder
    /// </summary>
    public class ConcreteTypeFilter : IConcreteTypeFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteTypeFilter"/> class.
        /// </summary>
        /// <param name="types">The current service types</param>
        public ConcreteTypeFilter(IEnumerable<Type> types)
        {
            FilteredTypes = types.Where(t => t.IsConcreteType());
        }


        /// <summary>
        /// Gets the current filtered types
        /// </summary>
        public IEnumerable<Type> FilteredTypes { get; private set; }


        /// <summary>
        /// Filter to types that are assignable to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type that should be assignable.</typeparam>
        /// <returns>An <see langword="interface"/> to filter service types</returns>
        public IConcreteTypeFilter AssignableTo<T>()
        {
            return AssignableTo(typeof(T));
        }

        /// <summary>
        /// Filter to types that are assignable to the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type that should be assignable.</param>
        /// <returns>An <see langword="interface"/> to filter service types</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="type"/> argument is <c>null</c>.</exception>
        public IConcreteTypeFilter AssignableTo(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return AssignableToAny(type);
        }

        /// <summary>
        /// Filter to types that are assignable to any of the specified <paramref name="types" />.
        /// </summary>
        /// <param name="types">>The types that should be assignable.</param>
        /// <returns>An <see langword="interface"/> to filter service types</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
        public IConcreteTypeFilter AssignableToAny(params Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            return AssignableToAny(types.AsEnumerable());
        }

        /// <summary>
        /// Filter to types that are assignable to any of the specified <paramref name="types" />.
        /// </summary>
        /// <param name="types">>The types that should be assignable.</param>
        /// <returns>An <see langword="interface"/> to filter service types</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
        public IConcreteTypeFilter AssignableToAny(IEnumerable<Type> types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            return Where(t => types.Any(v => AssemblyExtensions.IsAssignableTo(t, v)));
        }

        /// <summary>
        /// Filter types based on the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate to filter types.</param>
        /// <returns>An <see langword="interface"/> to filter service types</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate" /> argument is <c>null</c>.</exception>
        public IConcreteTypeFilter Where(Func<Type, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            FilteredTypes = FilteredTypes.Where(predicate);

            return this;
        }
    }
}
