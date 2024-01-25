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
    public class CategoryController(IMapper mapper, ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IMapper _mapper = mapper;

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category is null)
                return NotFound("Category Not Found");

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategories()
        {
            var categories = await _categoryService.GetAll();

            if (categories is null || categories.Count == 0)
                return NotFound("Categories Not Found");

            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            var existingCategory = await _categoryService.Update(category);

            if (existingCategory is null)
                return NotFound("Category Not Found");

            return Ok(_mapper.Map<CategoryDto>(existingCategory));
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var existingCategory = await _categoryService.Create(category);

            if (existingCategory is null)
                return BadRequest("Category was not created");

            return Ok("Category Successfully Added");
        }
    }
}
