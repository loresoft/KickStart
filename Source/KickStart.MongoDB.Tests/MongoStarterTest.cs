using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson.Serialization;
using Test.Core;
using Xunit;

namespace KickStart.MongoDB.Tests
{
    public class MongoStarterTest
    {
        [Fact]
        public void Configure()
        {
            Kick.Start(config => config
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
}
