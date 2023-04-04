using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using URLShortener.BLL.DTO;
using URLShortener.DAL.Repositories.Interfaces.Base;

namespace URLShortener.BLL.MediatR.Url.GetById
{
    public class GetUrlByIdHandler : IRequestHandler<GetUrlByIdQuery, Result<UrlDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetUrlByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<UrlDTO>> Handle(GetUrlByIdQuery request, CancellationToken cancellationToken)
        {
            var facts = await _repositoryWrapper.UrlRepository
                .GetFirstOrDefaultAsync(f => f.Id == request.Id, include: p => p.Include(s => s.User));

            if (facts is null)
            {
                return Result.Fail(new Error($"Cannot find any fact with corresponding id: {request.Id}"));
            }

            var factsDto = _mapper.Map<UrlDTO>(facts);
            return Result.Ok(factsDto);
        }
    }
}
