using System;
using System.Collections.Generic;

namespace KickStart
{
    /// <summary>
    /// A class defining the KickStart configuration.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            Assemblies = new AssemblyResolver();

            // exclude system assemblies
            Assemblies.ExcludeName("mscorlib");
            Assemblies.ExcludeName("Microsoft");
            Assemblies.ExcludeName("System");
            
            // exclude self
            Assemblies.ExcludeAssemblyFor<IKickStarter>();

            Starters = new List<IKickStarter>();
        }

        /// <summary>
        /// Gets the assemblies used by KickStart.
        /// </summary>
        /// <value>
        /// The assemblies use by KickStart.
        /// </value>
        public AssemblyResolver Assemblies { get; }

        /// <summary>
        /// Gets the <see cref="IKickStarter"/> extensions to use.
        /// </summary>
        /// <value>
        /// The IKickStarter extensions to use.
        /// </value>
        public List<IKickStarter> Starters { get; }
    }
}