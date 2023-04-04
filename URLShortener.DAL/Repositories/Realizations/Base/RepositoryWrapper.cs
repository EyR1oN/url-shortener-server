using URLShortener.DAL.Entities;
using URLShortener.DAL.Repositories.Interfaces;
using URLShortener.DAL.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DAL.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DataContext _dataContext;

        private IUrlRepository _urlRepository;

        private IUserRepository _userRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_dataContext);
                }
                return _userRepository;
            }
        }

        public IUrlRepository UrlRepository
        {
            get
            {
                if (_urlRepository is null)
                {
                    _urlRepository = new UrlRepository(_dataContext);
                }
                return _urlRepository;
            }
        }

        public RepositoryWrapper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
