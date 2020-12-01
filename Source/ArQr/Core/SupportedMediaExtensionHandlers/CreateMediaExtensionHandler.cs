using System.Threading;
using System.Threading.Tasks;
using ArQr.Interface;
using AutoMapper;
using Data.Repository.Base;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Resource.Api.Resources;

namespace ArQr.Core.SupportedMediaExtensionHandlers
{
    public sealed record CreateMediaExtensionRequest(CreateSupportedMediaExtensionResource ExtensionResource) :
        IRequest<ActionHandlerResult>;

    public class CreateMediaExtensionHandler : IRequestHandler<CreateMediaExtensionRequest, ActionHandlerResult>
    {
        private readonly IUnitOfWork       _unitOfWork;
        private readonly IResponseMessages _responseMessages;
        private readonly IMapper           _mapper;

        public CreateMediaExtensionHandler(IUnitOfWork       unitOfWork,
                                           IResponseMessages responseMessages,
                                           IMapper           mapper)
        {
            _unitOfWork       = unitOfWork;
            _responseMessages = responseMessages;
            _mapper           = mapper;
        }

        public async Task<ActionHandlerResult> Handle(CreateMediaExtensionRequest request,
                                                      CancellationToken           cancellationToken)
        {
            var extensionName = request.ExtensionResource.Extension;

            var extension = await _unitOfWork.SupportedMediaExtensionRepository.GetAsync(extensionName);
            if (extension is not null) return new(StatusCodes.Status200OK, _responseMessages.DuplicateExtension());

            var extensionToInsert = _mapper.Map<SupportedMediaExtension>(request.ExtensionResource);
            await _unitOfWork.SupportedMediaExtensionRepository.InsertAsync(extensionToInsert);
            await _unitOfWork.CompleteAsync();

            return new(StatusCodes.Status201Created, extensionToInsert);
        }
    }
}