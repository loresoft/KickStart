using MongoDB.Bson.Serialization;

using Test.Core;

namespace KickStart.MongoDB.Tests;

public class MongoStarterTest
{
    private readonly ITestOutputHelper _output;

    public MongoStarterTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Configure()
    {
        Kick.Start(config => config
            .LogTo(_output.WriteLine)
            .IncludeAssemblyFor<UserMap>()
            .UseMongoDB()
        );

        var isMapped = BsonClassMap.IsClassMapRegistered(typeof(User));
        isMapped.Should().BeTrue();

        var map = BsonClassMap.LookupClassMap(typeof(User));
        map.Should().NotBeNull();
        map.IdMemberMap.Should().NotBeNull();
        map.IdMemberMap.MemberName.Should().Be("Id");
    }
}
