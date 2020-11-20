using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using AutoMapper;
using Data.Repository.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core
{
    public sealed record UserGetMeRequest : IRequest<UserGetMeResult>;

    public sealed record UserGetMeResult(int StatusCode, object Value);

    public class UserGetMeHandler : IRequestHandler<UserGetMeRequest, UserGetMeResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IMapper              _mapper;

        public UserGetMeHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _mapper              = mapper;
        }

        public async Task<UserGetMeResult> Handle(UserGetMeRequest request, CancellationToken cancellationToken)
        {
            var (isAuthenticated, userId) = _httpContextAccessor.HttpContext!.GetUserAuthentication();
            if (isAuthenticated is false) return new(StatusCodes.Status401Unauthorized, "Unauthorized.");

            var user = await _unitOfWork.UserRepository.GetAsync(userId);
            return user is null
                       ? new(StatusCodes.Status404NotFound, "User Not Found")
                       : new(StatusCodes.Status200OK, _mapper.Map<UserResource>(user));
        }
    }
}