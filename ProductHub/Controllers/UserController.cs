using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Database.Contract;
using ProductHub.Database.Entities;
using ProductHub.Database.Services;
using ProductHub.Model.Dto;

namespace ProductHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMapper mapper, IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetById(id);

            if (user is null)
                return NotFound("User Not Found");

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUsers()
        {
            var users = await _userService.GetAll();

            if (users is null)
                return NotFound("Users Not Found");

            return Ok(_mapper.Map<List<UserDto>>(users));
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var user = _mapper.Map<User>(updateUserDto);
            var existingUser = await _userService.Update(user);

            if (existingUser is null)
                return NotFound("User Not Found");

            return Ok(_mapper.Map<UserDto>(existingUser));
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            var existingUser = await _userService.Create(user);

            if (existingUser is null)
                return BadRequest("User was not created");

            return Ok("User Successfully Added");
        }
    }
}

