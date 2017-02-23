using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Test.Core
{
    public class UserService : IUserService
    {

        public IConnection Connection { get; }

        public UserService(IConnection connection)
        {
            Connection = connection;
        }


        public User Add(User entity)
        {
            return null;
        }

        public bool Update(User entity)
        {
            return false;
        }

        public bool Delete(User entity)
        {
            return false;
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            return null;
        }

        public ICollection<User> GetAll()
        {
            return null;
        }
    }
}
