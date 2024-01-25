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
    public class UserService(DBContext context) : IUserService
    {
        private readonly DBContext _context = context;

        public async Task<User?> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser is not null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                await _context.SaveChangesAsync();
            }

            return existingUser;
        }
    }
}
