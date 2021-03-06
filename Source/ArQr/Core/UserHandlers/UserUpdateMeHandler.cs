using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Resource.Api.Resources;

namespace ArQr.Core.UserHandlers
{
    public sealed record UserUpdateMeRequest(UserUpdateResource UpdateResource) : IRequest<ActionHandlerResult>;

    public class UserUpdateMeHandler : IRequestHandler<UserUpdateMeRequest, ActionHandlerResult>
    {
        private readonly IHttpContextAccessor  _httpContextAccessor;
        private readonly IUnitOfWork           _unitOfWork;
        private readonly IResponseMessages     _responseMessages;
        private readonly IMapper               _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserUpdateMeHandler(IHttpContextAccessor  httpContextAccessor,
                                   IUnitOfWork           unitOfWork,
                                   IResponseMessages     responseMessages,
                                   IMapper               mapper,
                                   IPasswordHasher<User> passwordHasher)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork          = unitOfWork;
            _responseMessages    = responseMessages;
            _mapper              = mapper;
            _passwordHasher      = passwordHasher;
        }

        public async Task<ActionHandlerResult> Handle(UserUpdateMeRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext!.GetUserId();
            var user   = await _unitOfWork.UserRepository.GetAsync(userId);
            if (user is null) return new(StatusCodes.Status404NotFound, _responseMessages.UserNotFound());

            var updateResource = request.UpdateResource;
            user = _mapper.Map(updateResource, user);
            if (updateResource.Password is not null)
                user.PasswordHash = _passwordHasher.HashPassword(user, updateResource.Password);

            _unitOfWork.UserRepository.Update(user);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return (e.InnerException as SqlException)?.Number == 2601
                           ? new(StatusCodes.Status409Conflict,
                                 _responseMessages.DuplicatePhoneNumber())
                           : new(StatusCodes.Status500InternalServerError,
                                 _responseMessages.UnhandledException());
            }

            return new(StatusCodes.Status200OK, _mapper.Map<UserResource>(user));
        }
    }
}