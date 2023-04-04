using URLShortener.DAL.Entities;
using URLShortener.DAL.Repositories.Interfaces;
using URLShortener.DAL.Repositories.Realizations.Base;

namespace URLShortener.DAL.Repositories.Realizations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext)
        : base(dbContext)
        {
        }
    }
}
