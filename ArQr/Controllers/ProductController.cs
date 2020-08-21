using System;
using System.Linq;
using System.Threading.Tasks;
using ArQr.Controllers.Resources;
using ArQr.Data;
using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Localization.ErrorKeys;
using ArQr.Models;
using ArQr.Models.Repositories;
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
        private readonly IProductRepository         _productRepository;
        private readonly IMapper                    _mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public ProductController(IProductRepository         productRepository,
                                 IMapper                    mapper,
                                 IStringLocalizer<Resource> localizer)
        {
            _productRepository = productRepository;
            _mapper            = mapper;
            _localizer         = localizer;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var userId  = HttpContext.GetUserId();
            var product = await _productRepository.GetProductAsync(id);
            if (product is null) return ApiResponse.NotFound(_localizer.GetProductError(ProductErrors.NotFound));
            if (product.OwnerId != userId)
                return ApiResponse.UnAuthorize(_localizer.GetUserError(UserErrors.UnAuthorize));

            return ApiResponse.Ok(_mapper.Map<ProductResource>(product));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProducts(int take = 20, int after = 0)
        {
            var userId       = HttpContext.GetUserId();
            var userProducts = await _productRepository.GetProductsByUserIdAsync(userId, take, after);
            if (!userProducts.Any()) return ApiResponse.NotFound(_localizer.GetProductError(ProductErrors.NotFound));

            return ApiResponse.Ok(userProducts);
        }
    }
}