using FluentResults;
using URLShortener.BLL.DTO;
using MediatR;

namespace URLShortener.BLL.MediatR.User.GetByLoginParams
{
    public record GetUsersByLoginParamsQuery(string username, string password) : IRequest<Result<UserDTO>>;
}
