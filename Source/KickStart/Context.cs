using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KickStart
{
    /// <summary>
    /// The KickStart running context.
    /// </summary>
    public class Context
    {
        private readonly IReadOnlyList<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public Context(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IContainerAdaptor Container
        {
            get { return Kick.Container; }
        }

        /// <summary>
        /// Gets the assemblies used by KickStart.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        public IReadOnlyList<Assembly> Assemblies
        {
            get { return _assemblies; }
        }

        /// <summary>
        /// Sets the global <see cref="P:KickStart.Kick.Container"/>.
        /// </summary>
        /// <param name="container">The container adaptor to assign.</param>
        public void SetContainer(IContainerAdaptor container)
        {
            Logger.Verbose()
               .Message("Assign Kick Container: {0}", container)
               .Write();
            
            Kick.SetContainer(container);
        }
    }
}