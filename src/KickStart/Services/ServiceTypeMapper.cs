using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KickStart.Services
{
    /// <summary>
    /// Service type map builder
    /// </summary>
    public class ServiceTypeMapper : IServiceTypeMapper
    {
        private readonly IEnumerable<Type> _concreteTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeMapper"/> class.
        /// </summary>
        /// <param name="concreteTypes">The current service types</param>
        public ServiceTypeMapper(IEnumerable<Type> concreteTypes)
        {
            _concreteTypes = concreteTypes ?? throw new ArgumentNullException(nameof(concreteTypes));
            TypeMaps = new List<TypeMap>();
        }

        /// <summary>
        /// Gets a list of type maps to register.
        /// </summary>
        public List<TypeMap> TypeMaps { get; }

        /// <summary>
        /// Registers each concrete type as itself.
        /// </summary>
        /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
        public IServiceTypeMapper Self()
        {
            var typeMaps = _concreteTypes.Select(t => new TypeMap(t, new[] { t }));
            TypeMaps.AddRange(typeMaps);

            return this;
        }

        /// <summary>
        /// Registers each concrete type as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to register as.</typeparam>
        /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
        public IServiceTypeMapper Type<T>()
        {
            return Types(typeof(T));
        }

        /// <summary>
        /// Registers each concrete type as each of the specified <paramref name="types" />.
        /// </summary>
        /// <param name="types">The types to register as.</param>
        /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
        public IServiceTypeMapper Types(params Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            return Types(types.AsEnumerable());
        }

        /// <summary>
        /// Registers each concrete type as each of the specified <paramref name="types" />.
        /// </summary>
        /// <param name="types">The types to register as.</param>
        /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="types"/> argument is <c>null</c>.</exception>
        public IServiceTypeMapper Types(IEnumerable<Type> types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            var typeMaps = _concreteTypes.Select(t => new TypeMap(t, types));
            TypeMaps.AddRange(typeMaps);

            return this;
        }

        /// <summary>
        /// Registers each concrete type as all of its implemented interfaces.
        /// </summary>
        /// <returns>An <see langword="interface"/> to configure how implementations are registered.</returns>
        public IServiceTypeMapper ImplementedInterfaces()
        {
            var typeMaps = _concreteTypes.Select(t => new TypeMap(t, t.GetTypeInfo().GetInterfaces()));
            TypeMaps.AddRange(typeMaps);

            return this;
        }
    }
}