using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Test.Core
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Employee Add(Employee entity)
        {
            return null;
        }

        public bool Update(Employee entity)
        {
            return false;
        }

        public bool Delete(Employee entity)
        {
            return false;
        }

        public Employee Get(Expression<Func<Employee, bool>> filter)
        {
            return null;
        }

        public ICollection<Employee> GetAll()
        {
            return null;
        }
    }
}