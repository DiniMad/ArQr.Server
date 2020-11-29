using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.UserHandlers
{
    public sealed record UserGetMeRequest : IRequest<ActionHandlerResult>;

    public class UserGetMeHandler : IRequestHandler<UserGetMeRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IResponseMessages    _responseMessages;
        private readonly IMapper              _mapper;

        public UserGetMeHandler(IHttpContextAccessor httpContextAccessor,
                                IUnitOfWork          unitOfWork,
                                IResponseMessages    responseMessages,
                                IMapper              mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _mapper              = mapper;
        }

        public async Task<ActionHandlerResult> Handle(UserGetMeRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.GetUserId();

            var user = await _unitOfWork.UserRepository.GetAsync(userId);
            return user is null
                       ? new(StatusCodes.Status404NotFound, _responseMessages.UserNotFound())
                       : new(StatusCodes.Status200OK,
                             _mapper.Map<UserResource>(user));
        }
    }
}