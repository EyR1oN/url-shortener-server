using URLShortener.DAL.Entities;
using URLShortener.DAL.Repositories.Interfaces;
using URLShortener.DAL.Repositories.Realizations.Base;

namespace URLShortener.DAL.Repositories.Realizations
{
    public class UrlRepository : RepositoryBase<Url>, IUrlRepository
    {
        public UrlRepository(DataContext dbContext)
        : base(dbContext)
        {
        }
    }
}
