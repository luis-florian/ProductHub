using Microsoft.EntityFrameworkCore;
using ProductHub.Database.Context;
using ProductHub.Database.Contract;
using ProductHub.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Services
{
    public class UserService : IUserService
    {
        private readonly DBContext _context;
        public UserService(DBContext context)
        {
            this._context = context;
        }
        public async Task<User?> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<List<User?>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> Update(User user)
        {
            var _user = await _context.Users.FindAsync(user.Id);

            if (_user is not null)
            {
                _user.Name = user.Name;
                _user.Email = user.Email;

                await _context.SaveChangesAsync();
            }

            return _user;
        }
    }
}
