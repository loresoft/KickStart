using System;

namespace KickStart.EntityChange
{
    /// <summary>
    /// KickStart EntityChange options.
    /// </summary>
    public class EntityChangeOptions
    {
        /// <summary>
        /// Gets or sets the <see langword="delegate"/> to call for additional configuration.
        /// </summary>
        /// <value>
        /// The <see langword="delegate"/> to call for additional configuration.
        /// </value>
        public Action<global::EntityChange.Fluent.ConfigurationBuilder> Configure { get; set; }
    }
}