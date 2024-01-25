using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Database.Contract;
using ProductHub.Database.Entities;
using ProductHub.Model;
using ProductHub.Model.Dto;
using ProductHub.Storage.Contract;
using System.Transactions;

namespace ProductHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMapper mapper, IProductService productService, IImageFileService imageService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IImageFileService _imageFileService = imageService;
        private readonly IMapper _mapper = mapper;

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetById(id);

            if (product is null)
                return NotFound("Product Not Found");

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet]
        public async Task<ActionResult<ProductsDto>> GetProducts()
        {
            var products = await _productService.GetAll();

            if (products is null)
                return NotFound("Products Not Found");

            return Ok(_mapper.Map<List<ProductsDto>>(products));
        }

        [Route("category/{idCategory}")]
        [HttpGet]
        public async Task<ActionResult<ProductsDto>> GetProductsByCategory(int idCategory)
        {
            var products = await _productService.GetProductsByCategory(idCategory);

            if (products is null)
                return NotFound("Products Not Found");

            return Ok(_mapper.Map<List<ProductsDto>>(products));
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var existingProduct = await _productService.Update(product);

            if (existingProduct is null)
                return NotFound("Product Not Found");

            return Ok(_mapper.Map<ProductDto>(existingProduct));
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var existingProduct = await _productService.Create(product);

            if (existingProduct is null)
                return BadRequest("Product was not created");

            return Ok("Product Successfully Added");
        }

        [HttpPost]
        [Route("comments")]
        public async Task<ActionResult> AddCommentProduct([FromBody] CreateCommentDto createCommenttDto)
        {
            var comment = _mapper.Map<Comment>(createCommenttDto);
            var product = await _productService.AddCommentToProduct(comment);

            if (product is null)
                return BadRequest("Product was not created");

            return Ok("Comment Successfully Added");
        }

        [HttpPost]
        [Route("images")]
        public async Task<ActionResult> AddImagesProduct([FromForm] FileUpload file)
        {
            if (file == null || file.Files.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Files.FileName);

            using var stream = file.Files.OpenReadStream();
            var resultImageFileOperation = await _imageFileService.Upload(fileName, stream);

            if (!resultImageFileOperation.ProcessedSuccessfully)
                return BadRequest($"The file was not uploaded: {resultImageFileOperation.Message}");

            CreateImageDto createImageDto = new() { Url = resultImageFileOperation.FilePath, ProductId = file.ProductId };

            var image = _mapper.Map<Image>(createImageDto);

            var resultProductSerive = await _productService.AddImagesToProduct([image]);

            if (resultProductSerive is null)
                return BadRequest("Image was not created");

            return Ok("Image Successfully Added");
        }

        [HttpDelete]
        [Route("comment")]
        public async Task<ActionResult> DeleteCommentFromProduct([FromBody] DeleteCommentDto deleteCommentDto)
        {
            var comment = _mapper.Map<Comment>(deleteCommentDto);
            var product = await _productService.DeleteCommentToProduct(comment);

            if (product is null)
                return BadRequest("Comment was not deleted");

            return Ok("Comment Successfully Deleted");
        }

        [HttpDelete]
        [Route("Image")]
        public async Task<ActionResult> DeleteImageFromProduct([FromBody] DeleteImageDto deleteImageDto)
        {
            var imageResult = await _productService.GetImageFromProduct(deleteImageDto.ProductId, deleteImageDto.Id);

            if (imageResult is null)
                return NotFound("Image Not Found");

            var resultImageFileOperation = await _imageFileService.Delete(imageResult.Url);

            if (!resultImageFileOperation.ProcessedSuccessfully)
                return BadRequest($"The file was not deleted: {resultImageFileOperation.Message}");

            var image = _mapper.Map<Image>(deleteImageDto);
            var product = await _productService.DeleteImageToProduct(image);

            if (product is null)
                return BadRequest("Image was not deleted");

            return Ok("Image Successfully Deleted");
        }
    }
}
