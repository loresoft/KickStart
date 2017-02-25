using System;
using Autofac;
using Autofac.Builder;

namespace KickStart.Autofac
{
    /// <summary>
    /// Autofac fluent builder
    /// </summary>
    /// <seealso cref="KickStart.Autofac.IAutofacBuilder" />
    public class AutofacBuilder : IAutofacBuilder
    {
        private readonly AutofacOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacBuilder"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AutofacBuilder(AutofacOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Sets Autofac build options.
        /// </summary>
        /// <param name="buildOptions">The build options.</param>
        /// <returns></returns>
        public IAutofacBuilder Options(ContainerBuildOptions buildOptions)
        {
            _options.BuildOptions = buildOptions;
            return this;
        }

        /// <summary>
        /// Sets the initialize builder <see langword="delegate" />.  Register services under this <see langword="delegate" />.
        /// </summary>
        /// <param name="initializer">The initialize builder <see langword="delegate" />.</param>
        /// <returns></returns>
        public IAutofacBuilder Initialize(Action<ContainerBuilder> initializer)
        {
            _options.Initializer = initializer;
            return this;
        }

        /// <summary>
        /// Sets the container accessor <see langword="delegate" />.  Resolve services under this <see langword="delegate" />.
        /// </summary>
        /// <param name="accessor">The container accessor <see langword="delegate" />.</param>
        /// <returns></returns>
        public IAutofacBuilder Container(Action<IContainer> accessor)
        {
            _options.Accessor = accessor;
            return this;
        }

    }
}
