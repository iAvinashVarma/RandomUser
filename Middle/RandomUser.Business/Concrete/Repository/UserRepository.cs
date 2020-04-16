using RandomUser.Business.Contract.Repository;
using RandomUser.Business.Entity;
using RandomUser.Business.Insfrastructure.Base;
using RandomUser.Business.Model;
using System;
using System.Linq;

namespace RandomUser.Business.Concrete.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public User GetUserByFirstName(string firstName)
        {
            return GetByCondition(u => u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
        }

        public User GetUserByLastName(string lastName)
        {
            return GetByCondition(u => u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
        }
    }
}
