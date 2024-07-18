using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Entites.Identity;
using Order_Manag.Core.Repository.Contract;
using Order_Manag.Core.Specifications;
using Order_Manag.Core.Specifications.ProductsSpec;
using Order_Management.DTOS;
using Order_Management.HandlingErrors;
using System.Security.Claims;

namespace Order_Management.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProduct(string? Sort)
        {
            var Spec = new ProductWithIncludeAndQuerySpec(Sort);

            var product = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProduct = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(product);
            return Ok(MappedProduct);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Spec = new ProductWithIncludeAndQuerySpec(id);
            var product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (product is null) return NotFound(new ApisResponse(404));
            var MappedProduct = _mapper.Map<Product, ProductDto>(product);
            return Ok(MappedProduct);

        }
        
        [HttpPost()]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> Add(ProductRequestDto productRequestDto)
        {
           
                var product = await _productRepo.AddAsync(new Product()
                {
                    Name = productRequestDto.Name,
                    Price = productRequestDto.Price,
                    Stock = productRequestDto.Stock,
                });
                await _productRepo.SaveChangesAsync();
                if (product is null) return NotFound(new ApisResponse(404));
                var MappedProduct = _mapper.Map<Product, ProductDto>(product);
                return Ok(MappedProduct);

        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> Update([FromRoute] int id, ProductRequestDto productRequestDto)
        {
            var Spec = new ProductWithIncludeAndQuerySpec(id);
            var oldProduct = await _productRepo.GetByIdWithSpecAsync(Spec);
            oldProduct.Price = productRequestDto.Price;
            oldProduct.Stock = productRequestDto.Stock;
            oldProduct.Name = productRequestDto.Name;
            var product = _productRepo.Update(oldProduct);
            await _productRepo.SaveChangesAsync();
            if (product is null) return NotFound(new ApisResponse(404));
            var MappedProduct = _mapper.Map<Product, ProductDto>(product);
            return Ok(MappedProduct);

        }
    }
}
