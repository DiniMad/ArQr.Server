using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Resource.Api.Resources;

namespace ArQr.Core
{
    public sealed record RegisterUserRequest(UserRegisterResource RegisterResource) : IRequest<ActionHandlerResult>;

    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, ActionHandlerResult>
    {
        private readonly IMapper               _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork           _unitOfWork;

        public RegisterUserHandler(IMapper mapper, IPasswordHasher<User> passwordHasher, IUnitOfWork unitOfWork)
        {
            _mapper         = mapper;
            _passwordHasher = passwordHasher;
            _unitOfWork     = unitOfWork;
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
                           ? new(StatusCodes.Status409Conflict, "Phone Number already taken.")
                           : new(StatusCodes.Status500InternalServerError, "Unhandled Exception.");
            }

            return new(StatusCodes.Status201Created, _mapper.Map<UserResource>(user));
        }
    }
}