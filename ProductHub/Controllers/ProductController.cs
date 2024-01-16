using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Database.Contract;
using ProductHub.Database.Entities;
using ProductHub.Model.Dto;

namespace ProductHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IMapper mapper, IProductService productService)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.Get(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProducts()
        {
            var products = await _productService.Get();

            if (products is null)
            {
                return NotFound("Products Not Found");
            }

            var productDto = _mapper.Map<List<ProductDto>>(products);

            return Ok(productDto);
        }

        [Route("category/{idCategory}")]
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProductsByCategory(int idCategory)
        {
            var products = await _productService.GetProductsByCategory(idCategory);

            if (products is null)
            {
                return NotFound("Products Not Found");
            }

            var productDto = _mapper.Map<List<ProductDto>>(products);

            return Ok(productDto);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var _product = await _productService.Update(product);

            if (_product is null)
                return NotFound("Product not found.");

            var ProductDto = _mapper.Map<ProductDto>(_product);

            return Ok(ProductDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var _product = await _productService.Create(product);

            var ProductDto = _mapper.Map<ProductDto>(_product);

            return Ok(ProductDto);
        }

        [HttpPost]
        [Route("comments")]
        public async Task<ActionResult<ProductDto>> AddCommentProduct([FromBody] CreateCommentDto createCommenttDto)
        {
            var comment = _mapper.Map<Comment>(createCommenttDto);
            var _product = await _productService.AddCommentToProduct(comment);

            var ProductDto = _mapper.Map<ProductDto>(_product);

            return Ok(ProductDto);
        }

        [HttpPost]
        [Route("images")]
        public async Task<ActionResult<ProductDto>> AddImagesProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var _product = await _productService.Create(product);

            var ProductDto = _mapper.Map<ProductDto>(_product);

            return Ok(ProductDto);
        }
    }
}
