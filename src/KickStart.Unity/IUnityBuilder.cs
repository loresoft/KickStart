using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{

    /// <summary>
    /// Unity builder interface
    /// </summary>
    public interface IUnityBuilder
    {
        /// <summary>
        /// Sets the initialize container <see langword="delegate"/>.
        /// </summary>
        /// <param name="initializer">The initializer the container.</param>
        /// <returns></returns>
        IUnityBuilder Container(Action<IUnityContainer> initializer);

    }
}