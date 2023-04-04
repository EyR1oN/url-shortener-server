using AutoMapper;
using FluentResults;
using URLShortener.BLL.DTO;
using URLShortener.DAL.Repositories.Interfaces.Base;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace URLShortener.BLL.MediatR.Questionnaire.Get
{
    public class GetQestionnaireHandler : IRequestHandler<GetUrlByShortUrlQuery, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetQestionnaireHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(GetUrlByShortUrlQuery request, CancellationToken cancellationToken)
        {
            var url = await _repositoryWrapper
                .UrlRepository
                .GetFirstOrDefaultAsync(c => c.ShortenedUrl == request.shortUrl, 
                    include: q => q.Include(s => s.User));

            if (url is null)
            {
                return Result.Fail(new Error($"Cannot find any url"));
            }

            return Result.Ok(url.OriginalUrl);
        }
    }
}
