namespace KickStart.EntityChange;

/// <summary>
/// Fluent <see cref="EntityChangeOptions"/> builder.
/// </summary>
public class EntityChangeBuilder : IEntityChangeBuilder
{
    private readonly EntityChangeOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityChangeBuilder"/> class.
    /// </summary>
    /// <param name="options">The EntityChangeOptions to configure.</param>
    public EntityChangeBuilder(EntityChangeOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Passes the current <see cref="T:EntityChange.IConfiguration" /> to add additional configuration options.
    /// </summary>
    /// <param name="configuration">The delegate to call for additional configuration.</param>
    /// <returns>
    /// Fluent <see cref="EntityChangeOptions" /> builder.
    /// </returns>
    public IEntityChangeBuilder Configure(Action<global::EntityChange.Fluent.ConfigurationBuilder> configuration)
    {
        _options.Configure = configuration;
        return this;
    }

}