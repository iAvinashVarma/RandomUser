namespace RandomUser.Business.Contract.Repository
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        void Save();
    }
}
