using FluentResults;
using MediatR;

namespace URLShortener.BLL.MediatR.Url.Delete
{
    public record DeleteUrlByIdCommand(int Id) : IRequest<Result<Unit>>;
}
