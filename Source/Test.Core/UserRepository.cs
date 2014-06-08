using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core
{
    public class UserRepository : IUserRepository
    {
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
