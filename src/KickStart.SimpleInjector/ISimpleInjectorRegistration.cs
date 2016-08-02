using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    /// <summary>
    /// SimpleInjector registration module
    /// </summary>
    public interface ISimpleInjectorRegistration
    {
        /// <summary>
        /// Register injections with the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        void Register(Container container);
    }
}