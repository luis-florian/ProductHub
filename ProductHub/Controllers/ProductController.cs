using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Database.Contract;

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
    }
}
