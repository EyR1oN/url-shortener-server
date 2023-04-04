using FluentResults;
using URLShortener.BLL.DTO;
using MediatR;

namespace URLShortener.BLL.MediatR.Url.GetAll
{
    public record GetAllUrlsQuery : IRequest<Result<IEnumerable<UrlDTO>>>;
}
