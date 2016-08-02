using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{

    /// <summary>
    /// Unity container registration interface
    /// </summary>
    public interface IUnityRegistration
    {
        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        void Register(IUnityContainer container);
    }
}