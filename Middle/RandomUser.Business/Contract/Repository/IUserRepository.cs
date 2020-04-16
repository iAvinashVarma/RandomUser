using RandomUser.Business.Model;

namespace RandomUser.Business.Contract.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByFirstName(string firstName);

        User GetUserByLastName(string lastName);
    }
}
