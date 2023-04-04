using FluentResults;
using MediatR;
using URLShortener.BLL.DTO;

namespace URLShortener.BLL.MediatR.Url.Create
{
    public record CreateUrlCommand(LongUrlDTO longUrl) : IRequest<Result<Unit>>;
}
