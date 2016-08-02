using System;
using Ninject;

namespace KickStart.Ninject
{
    /// <summary>
    /// Ninject options
    /// </summary>
    public class NinjectOptions
    {
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public INinjectSettings Settings { get; set; }
        /// <summary>
        /// Gets or sets the initialize kernel delegate.
        /// </summary>
        /// <value>
        /// The initialize kernel delegate.
        /// </value>
        public Action<IKernel> InitializeKernel { get; set; }
    }
}