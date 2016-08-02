using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    /// <summary>
    /// SimpleInjector configuration builder
    /// </summary>
    public class SimpleInjectorBuilder : ISimpleInjectorBuilder
    {
        private readonly SimpleInjectorOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorBuilder"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SimpleInjectorBuilder(SimpleInjectorOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Sets the initialize container <see langword="delegate" />.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        public ISimpleInjectorBuilder Container(Action<Container> initializer)
        {
            _options.InitializeContainer = initializer;
            return this;
        }
    }
}