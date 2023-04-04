using FluentResults;
using URLShortener.BLL.DTO;
using MediatR;

namespace URLShortener.BLL.MediatR.Questionnaire.Get
{
    public record GetUrlByShortUrlQuery(string shortUrl) : IRequest<Result<string>>;
}
