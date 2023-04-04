using AutoMapper;
using FluentResults;
using MediatR;
using System.Net.Http;
using System.Security.Claims;
using URLShortener.BLL.Services.Interfaces;
using URLShortener.DAL.Entities;
using URLShortener.DAL.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Http;

namespace URLShortener.BLL.MediatR.Url.Create
{
    public class CreateUrlHandler : IRequestHandler<CreateUrlCommand, Result<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IShortenUrlService _shortenUrlService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateUrlHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IShortenUrlService shortenUrlService, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _shortenUrlService = shortenUrlService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserIdAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim?.Value;
            if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out int userId))
            {
                throw new Exception("No such a User");
            }
            return userId;
        }

        public async Task<Result<Unit>> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {

            var shortUrl = _shortenUrlService.ShortenUrl(request.longUrl.Url);
            var userId = GetUserIdAsync();

            var Url = new DAL.Entities.Url()
            {
                ShortenedUrl = shortUrl,
                OriginalUrl = request.longUrl.Url,
                UserId = userId
            };

            await _repositoryWrapper.UrlRepository.CreateAsync(Url);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            return resultIsSuccess ? Result.Ok(Unit.Value) : Result.Fail(new Error("Failed to create a incident"));
        }
    }
}
