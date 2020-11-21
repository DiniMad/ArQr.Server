using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Resource.Api.Resources;
using Resource.ResourceFiles;

namespace ArQr.Core.UserController
{
    public sealed record UserGetMeRequest : IRequest<ActionHandlerResult>;

    public class UserGetMeHandler : IRequestHandler<UserGetMeRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor                   _httpContextAccessor;
        private readonly IUnitOfWork                            _unitOfWork;
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;
        private readonly IMapper                                _mapper;

        public UserGetMeHandler(IHttpContextAccessor                   httpContextAccessor,
                                IUnitOfWork                            unitOfWork,
                                IStringLocalizer<HttpResponseMessages> responseMessages,
                                IMapper                                mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _mapper              = mapper;
        }

        public async Task<ActionHandlerResult> Handle(UserGetMeRequest request, CancellationToken cancellationToken)
        {
            var (_, userId) = _httpContextAccessor.HttpContext!.GetUserAuthentication();

            var user = await _unitOfWork.UserRepository.GetAsync(userId);
            return user is null
                       ? new(StatusCodes.Status404NotFound,
                             _responseMessages[HttpResponseMessages.UserNotFound].Value)
                       : new(StatusCodes.Status200OK,
                             _mapper.Map<UserResource>(user));
        }
    }
}