using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using ArQr.Data.UnitOfWork;
using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Localization.ErrorKeys;
using ArQr.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ArQr.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork                _unitOfWork;
        private readonly IMapper                    _mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public ProductController(IUnitOfWork                unitOfWork,
                                 IMapper                    mapper,
                                 IStringLocalizer<Resource> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
            _localizer  = localizer;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var userId  = HttpContext.GetUserId();
            var product = await _unitOfWork.Products.GetAsync(id);
            if (product is null) return ApiResponse.NotFound(_localizer.GetProductError(ProductErrors.NotFound));
            if (product.OwnerId != userId)
                return ApiResponse.UnAuthorize(_localizer.GetUserError(UserErrors.UnAuthorize));

            return ApiResponse.Ok(_mapper.Map<ProductResource>(product));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProducts(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize   = Math.Max(pageSize,   1);

            var after        = (pageNumber - 1) * pageSize;
            var userId       = HttpContext.GetUserId();
            var userProducts = await _unitOfWork.Products.GetProductsByUserIdAsync(userId, pageSize, after);
            if (!userProducts.Any()) return ApiResponse.NotFound(_localizer.GetProductError(ProductErrors.NotFound));

            var productCount = await _unitOfWork.Products.CountAsync();
            var next = after + pageSize >= productCount
                           ? null
                           : Url.Action("GetUserProducts", "Product", new {pageNumber = pageNumber + 1, pageSize});
            var previous = after < 1
                               ? null
                               : Url.Action("GetUserProducts", "Product", new {pageNumber = pageNumber - 1, pageSize});

            var productResourceCollection = _mapper.Map<IReadOnlyList<ProductResource>>(userProducts);

            var result = new PaginationData<ProductResource>(productResourceCollection, next, previous);
            return ApiResponse.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductResource productResource)
        {
            var userId = HttpContext.GetUserId();

            var product = _mapper.Map<Product>(productResource);
            product.Id      = Guid.NewGuid().ToString();
            product.OwnerId = userId;

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.Complete();

            var location = Url.Action("GetProduct", "Product", new {id = product.Id});
            return ApiResponse.Created(location, _mapper.Map<ProductResource>(product));
        }
    }
}