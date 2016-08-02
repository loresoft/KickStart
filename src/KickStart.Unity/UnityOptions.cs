using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    /// <summary>
    /// Unity configuration options
    /// </summary>
    public class UnityOptions
    {
        /// <summary>
        /// Gets or sets the initialize container <see langword="delegate"/>.
        /// </summary>
        /// <value>
        /// The initialize container <see langword="delegate"/>.
        /// </value>
        public Action<IUnityContainer> InitializeContainer { get; set; }

    }
}
