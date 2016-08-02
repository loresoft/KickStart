using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    /// <summary>
    /// SimpleInjector configuration builder
    /// </summary>
    public interface ISimpleInjectorBuilder
    {
        /// <summary>
        /// Sets the initialize container <see langword="delegate"/>.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        ISimpleInjectorBuilder Container(Action<Container> initializer);
    }
}