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
    public class CategoryService(DBContext context) : ICategoryService
    {
        private readonly DBContext _context = context;

        public async Task<Category?> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> Update(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.Id);

            if (existingCategory is not null)
            {
                existingCategory.Name = category.Name;

                await _context.SaveChangesAsync();
            }

            return existingCategory;
        }
    }
}
