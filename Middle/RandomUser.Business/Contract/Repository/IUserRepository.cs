using RandomUser.Business.Model;
using System.Linq;

namespace RandomUser.Business.Contract.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IQueryable<User> GetUsersByLimit(int limit);

        IQueryable<User> GetUsersByFirstName(string firstName);

        IQueryable<User> GetUsersByLastName(string lastName);
    }
}
