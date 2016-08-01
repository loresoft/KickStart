using System;
using AutoMapper;

namespace KickStart.AutoMapper
{
    /// <summary>
    /// Fluent <see cref="AutoMapperOptions"/> builder.
    /// </summary>
    public class AutoMapperBuilder : IAutoMapperBuilder
    {
        private readonly AutoMapperOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperBuilder"/> class.
        /// </summary>
        /// <param name="options">The AutoMapperOptions to configure.</param>
        public AutoMapperBuilder(AutoMapperOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Call <see cref="M:AutoMapper.Mapper.AssertConfigurationIsValid" /> after configuration to validate the state of the mapping.
        /// </summary>
        /// <param name="value">if set to <c>true</c> to call to validate.</param>
        /// <returns>
        /// Fluent <see cref="AutoMapperOptions" /> builder.
        /// </returns>
        public IAutoMapperBuilder Validate(bool value = true)
        {
            _options.Validate = value;
            return this;
        }

        /// <summary>
        /// Passes the current <see cref="T:AutoMapper.IConfiguration" /> to add additional configuration options.
        /// </summary>
        /// <param name="configuration">The delegate to call for additional configuration.</param>
        /// <returns>
        /// Fluent <see cref="AutoMapperOptions" /> builder.
        /// </returns>
        public IAutoMapperBuilder Initialize(Action<IMapperConfigurationExpression> configuration)
        {
            _options.Initialize = configuration;
            return this;
        }        

    }
}