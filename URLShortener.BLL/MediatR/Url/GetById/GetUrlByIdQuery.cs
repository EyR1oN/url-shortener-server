using FluentResults;
using MediatR;
using URLShortener.BLL.DTO;

namespace URLShortener.BLL.MediatR.Url.GetById
{
    public record GetUrlByIdQuery(int Id) : IRequest<Result<UrlDTO>>;
}
