using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.UserHandlers
{
    public sealed record MakeUserAdminRequest(MakeUserAdminResource AdminResource) : IRequest<ActionHandlerResult>;

    public class MakeUserAdminHandler : IRequestHandler<MakeUserAdminRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork       _unitOfWork;
        private readonly IResponseMessages _responseMessages;

        public MakeUserAdminHandler(IUnitOfWork       unitOfWork,
                                    IResponseMessages responseMessages)
        {
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
        }

        public async Task<ActionHandlerResult> Handle(MakeUserAdminRequest request, CancellationToken cancellationToken)
        {
            var userId = request.AdminResource.UserId;
            var user   = await _unitOfWork.UserRepository.GetAsync(userId);
            if (user is null) return new(StatusCodes.Status404NotFound, _responseMessages.UserNotFound());

            user.Admin = request.AdminResource.Admin;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            return new(StatusCodes.Status200OK, _responseMessages.Done());
        }
    }
}