using System;
using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Resource.Api.Resources;

namespace ArQr.Core.AccountHandlers
{
    public sealed record RegisterUserRequest(UserRegisterResource RegisterResource) : IRequest<ActionHandlerResult>;

    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, ActionHandlerResult>
    {
        private readonly IMapper               _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork           _unitOfWork;
        private readonly IResponseMessages     _responseMessages;

        public RegisterUserHandler(IMapper               mapper,
                                   IPasswordHasher<User> passwordHasher,
                                   IUnitOfWork           unitOfWork,
                                   IResponseMessages     responseMessages)
        {
            _mapper           = mapper;
            _passwordHasher   = passwordHasher;
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
        }

        public async Task<ActionHandlerResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var registerResource = request.RegisterResource;
            var user             = _mapper.Map<User>(registerResource);
            user.PasswordHash = _passwordHasher.HashPassword(user, registerResource.Password);

            await _unitOfWork.UserRepository.InsertAsync(user);
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

            return new(StatusCodes.Status201Created, _mapper.Map<UserResource>(user));
        }
    }
}