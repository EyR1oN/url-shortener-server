using AutoMapper;
using FluentResults;
using URLShortener.BLL.DTO;
using URLShortener.DAL.Repositories.Interfaces.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace URLShortener.BLL.MediatR.Url.GetAll
{
    public class GetAllUrlsHandler : IRequestHandler<GetAllUrlsQuery, Result<IEnumerable<UrlDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetAllUrlsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UrlDTO>>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
        {
            var urls = await _repositoryWrapper
                .UrlRepository
                .GetAllAsync(include: p => p.Include(s => s.User));

            if (urls is null)
            {
                return Result.Fail(new Error($"Cannot find any Url"));
            }

            var urlsDtos = _mapper.Map<IEnumerable<UrlDTO>>(urls);
            return Result.Ok(urlsDtos);
        }
    }
}
