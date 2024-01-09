using Microsoft.EntityFrameworkCore;
using ProductHub.Database.Contract;
using ProductHub.Database.Entities;
using ProductHub.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductHub.Database.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DBContext _context;
        public CategoryService(DBContext context)
        {
            this._context = context;
        }
        public async Task<Category?> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> Get(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            return category;
        }

        public async Task<List<Category?>> Get()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> Update(Category category)
        {
            var _category = await _context.Categories.FindAsync(category.Id);

            if (_category is not null)
            {
                _category.Name = category.Name;

                await _context.SaveChangesAsync();
            }

            return _category;
        }
    }
}
