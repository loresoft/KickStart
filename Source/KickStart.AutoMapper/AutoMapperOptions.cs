using System;
using AutoMapper;

namespace KickStart.AutoMapper
{
    /// <summary>
    /// KickStart AutoMapper options.
    /// </summary>
    public class AutoMapperOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to validate AutoMapper.
        /// </summary>
        /// <value>
        ///   <c>true</c> to validate; otherwise, <c>false</c>.
        /// </value>
        public bool Validate { get; set; }

        /// <summary>
        /// Gets or sets the <see langword="delegate"/> to call for additional configuration.
        /// </summary>
        /// <value>
        /// The <see langword="delegate"/> to call for additional configuration..
        /// </value>
        public Action<IMapperConfigurationExpression> Initialize { get; set; }
    }
}