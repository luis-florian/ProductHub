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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper, ICategoryService categoryService) 
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.Get(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategories()
        {
            var categories = await _categoryService.Get();

            if (categories is null)
            {
                return NotFound("Categories Not Found");
            }

            var categoryDto = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(categoryDto);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            var _category = await _categoryService.Update(category);

            if (_category is null)
                return NotFound("Note not found.");

            var CategoryDto = _mapper.Map<CategoryDto>(_category);

            return Ok(CategoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var _category = await _categoryService.Create(category);

            var CategoryDto = _mapper.Map<CategoryDto>(_category);

            return Ok(CategoryDto);
        }
    }
}
