using System;
using System.Collections.Generic;

namespace KickStart.StartupTask
{
    /// <summary>
    /// A fluent <see langword="interface"/> to configure startup tasks
    /// </summary>
    public interface IStartupTaskBuilder
    {
        /// <summary>
        /// Runs the specified startup action.
        /// </summary>
        /// <param name="startupAction">The startup action.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure startup tasks
        /// </returns>
        IStartupTaskBuilder Run(Action<IServiceProvider, IDictionary<string, object>> startupAction);
    }

}