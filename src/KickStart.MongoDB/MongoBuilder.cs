namespace KickStart.MongoDB;

/// <summary>
/// MongoDB fluent builder
/// </summary>
/// <seealso cref="KickStart.MongoDB.IMongoBuilder" />
public class MongoBuilder : IMongoBuilder
{
    /// <summary>
    /// Gets the MongoDB configuration options.
    /// </summary>
    /// <value>
    /// The MongoDB configuration options..
    /// </value>
    public MongoOptions Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoBuilder"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public MongoBuilder(MongoOptions options)
    {
        Options = options;
    }
}