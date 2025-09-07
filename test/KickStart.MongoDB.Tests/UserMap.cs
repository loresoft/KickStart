using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

using Test.Core;

namespace KickStart.MongoDB.Tests
{
    public class UserMap : BsonClassMap<User>
    {
        public UserMap()
        {
            AutoMap();

            var idMember = GetMemberMap(p => p.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);

            SetIdMember(idMember);
        }
    }
}