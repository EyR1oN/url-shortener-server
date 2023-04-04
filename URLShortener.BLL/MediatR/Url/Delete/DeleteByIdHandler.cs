using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DAL.Repositories.Interfaces.Base;

namespace URLShortener.BLL.MediatR.Url.Delete
{
    public class DeleteFactHandler : IRequestHandler<DeleteUrlByIdCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteFactHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<Unit>> Handle(DeleteUrlByIdCommand request, CancellationToken cancellationToken)
        {
            var url = await _repositoryWrapper.UrlRepository.GetFirstOrDefaultAsync(f => f.Id == request.Id);

            if (url is null)
            {
                return Result.Fail(new Error($"Cannot find a fact with corresponding id: {request.Id}"));
            }

            _repositoryWrapper.UrlRepository.Delete(url);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            return resultIsSuccess ? Result.Ok(Unit.Value) : Result.Fail(new Error("Failed to delete a fact"));
        }
    }
}
