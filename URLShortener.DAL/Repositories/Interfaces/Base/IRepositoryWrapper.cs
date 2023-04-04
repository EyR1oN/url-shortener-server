namespace URLShortener.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper
    {
        IUrlRepository UrlRepository { get; }
        IUserRepository UserRepository { get; }

        public int SaveChanges();

        public Task<int> SaveChangesAsync();
    }
}
