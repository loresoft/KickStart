using System;
using System.Collections.Generic;

namespace KickStart
{
    /// <summary>
    /// An <see langword="interface"/> that defines an application KickStart extension
    /// </summary>
    public interface IKickStarter
    {
        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context"/> containing assemblies to scan.</param>
        void Run(Context context);
    }
}