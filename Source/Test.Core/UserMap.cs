using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Test.Core
{
    public class UserMap : BsonClassMap<User>
    {
        public UserMap()
        {
            AutoMap();

            var idMember = GetMemberMap(p => p.Id)
                .SetRepresentation(BsonType.ObjectId)
                .SetIdGenerator(StringObjectIdGenerator.Instance);

            SetIdMember(idMember);
        }
    }
}