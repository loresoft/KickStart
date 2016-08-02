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
        /// Sets the initialize builder <see langword="delegate" />.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        public IAutofacBuilder Builder(Action<ContainerBuilder> initializer)
        {
            _options.InitializeBuilder = initializer;
            return this;
        }

        /// <summary>
        /// Sets the initialize container <see langword="delegate" />.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        public IAutofacBuilder Container(Action<IContainer> initializer)
        {
            _options.InitializeContainer = initializer;
            return this;
        }

    }
}
