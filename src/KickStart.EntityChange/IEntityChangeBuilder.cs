namespace KickStart.EntityChange;

/// <summary>
/// Fluent <see cref="EntityChangeOptions"/> builder.
/// </summary>
public interface IEntityChangeBuilder
{
    /// <summary>
    /// Passes the current <see cref="T:EntityChange.IMapperConfigurationExpression"/> to add additional configuration options.
    /// </summary>
    /// <param name="configuration">The delegate to call for additional configuration.</param>
    /// <returns>
    /// Fluent <see cref="EntityChangeOptions"/> builder.
    /// </returns>
    IEntityChangeBuilder Configure(Action<global::EntityChange.Fluent.ConfigurationBuilder> configuration);
}