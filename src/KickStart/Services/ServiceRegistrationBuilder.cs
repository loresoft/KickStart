using System;
using System.Collections.Generic;

namespace KickStart.Services
{
    /// <summary>
    /// Service registration builder
    /// </summary>
    public class ServiceRegistrationBuilder : IServiceRegistrationBuilder
    {
        private readonly IEnumerable<Type> _types;
        private IEnumerable<Type> _concreteTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRegistrationBuilder"/> class.
        /// </summary>
        /// <param name="types">The current types to scan</param>
        public ServiceRegistrationBuilder(IEnumerable<Type> types)
        {
            _types = types;
        }

        /// <summary>
        /// Gets a list of type maps to register.
        /// </summary>
        public List<TypeMap> TypeMaps { get; private set; }


        /// <summary>
        /// Filters the types based on a <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see langword="interface"/> to build service registrations</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> argument is <c>null</c>.</exception>
        public IServiceRegistrationBuilder Types(Action<IConcreteTypeFilter> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var typeFilter = new ConcreteTypeFilter(_types);
            predicate(typeFilter);
            _concreteTypes = typeFilter.FilteredTypes;

            return this;
        }

        /// <summary>
        /// Configure how the types should be registered as.
        /// </summary>
        /// <param name="mapper">A function to configure how types should be registered.</param>
        /// <returns>An <see langword="interface"/> to build service registrations</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="mapper"/> argument is <c>null</c>.</exception>
        public IServiceRegistrationBuilder As(Action<IServiceTypeMapper> mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            var typeMapper = new ServiceTypeMapper(_concreteTypes);
            mapper(typeMapper);
            TypeMaps = typeMapper.TypeMaps;

            return this;
        }
    }
}
