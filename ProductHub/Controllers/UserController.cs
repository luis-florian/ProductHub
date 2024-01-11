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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.Get(id);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUsers()
        {
            var users = await _userService.Get();

            if (users is null)
            {
                return NotFound("Users Not Found");
            }

            var userDto = _mapper.Map<List<UserDto>>(users);

            return Ok(userDto);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var user = _mapper.Map<User>(updateUserDto);
            var _user = await _userService.Update(user);

            if (_user is null)
                return NotFound("User not found.");

            var UserDto = _mapper.Map<UserDto>(_user);

            return Ok(UserDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            var _user = await _userService.Create(user);

            var UserDto = _mapper.Map<UserDto>(_user);

            return Ok(UserDto);
        }
    }
}

