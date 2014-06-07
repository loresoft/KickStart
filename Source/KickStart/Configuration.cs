using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KickStart
{
    /// <summary>
    /// A class defining the KickStart configuration.
    /// </summary>
    public class Configuration
    {
        private readonly AssemblyResolver _assemblies;
        private readonly List<IKickStarter> _starters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            _assemblies = new AssemblyResolver();

            // exclude system assemblies
            _assemblies.ExcludeName("mscorlib");
            _assemblies.ExcludeName("Microsoft");
            _assemblies.ExcludeName("System");
            
            // exclude self
            _assemblies.ExcludeName("KickStart");

            _starters = new List<IKickStarter>();
        }

        /// <summary>
        /// Gets the assemblies used by KickStart.
        /// </summary>
        /// <value>
        /// The assemblies use by KickStart.
        /// </value>
        public AssemblyResolver Assemblies
        {
            get { return _assemblies; }
        }

        /// <summary>
        /// Gets the <see cref="IKickStarter"/> extensions to use.
        /// </summary>
        /// <value>
        /// The IKickStarter extensions to use.
        /// </value>
        public List<IKickStarter> Starters
        {
            get { return _starters; }
        }
    }
}