using System;
using AutoMapper;

namespace KickStart.AutoMapper
{
    /// <summary>
    /// Fluent <see cref="AutoMapperOptions"/> builder.
    /// </summary>
    public interface IAutoMapperBuilder
    {
        /// <summary>
        /// Call <see cref="M:AutoMapper.Mapper.AssertConfigurationIsValid"/> after configuration to validate the state of the mapping.
        /// </summary>
        /// <param name="value">if set to <c>true</c> to call to validate.</param>
        /// <returns>
        /// Fluent <see cref="AutoMapperOptions"/> builder.
        /// </returns>
        IAutoMapperBuilder Validate(bool value = true);

        /// <summary>
        /// Passes the current <see cref="T:AutoMapper.IMapperConfigurationExpression"/> to add additional configuration options.
        /// </summary>
        /// <param name="configuration">The delegate to call for additional configuration.</param>
        /// <returns>
        /// Fluent <see cref="AutoMapperOptions"/> builder.
        /// </returns>
        IAutoMapperBuilder Initialize(Action<IMapperConfigurationExpression> configuration);
    }
}