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
    public class ProductService : IProductService
    {
        private readonly DBContext _context;
        public ProductService(DBContext context)
        {
            this._context = context;
        }
        public async Task<Product> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<Product> Get(int id)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(c => c.Images)
                .Include(c => c.Comments)
                    .ThenInclude(c => c.User)
                .SingleOrDefaultAsync(p => p.Id == id);

            return product;
        }
        public async Task<List<Product>> Get()
        {
            return await _context.Products
                .Include(c => c.Category)
                .Include(c => c.Comments)
                .ToListAsync();
        }
        public async Task<Product> Update(Product product)
        {
            var _product = await _context.Products.FindAsync(product.Id);

            if (_product is not null)
            {
                _product.Name = product.Name;
                _product.Description = product.Description;
                _product.Stock = product.Stock;
                _product.Price = product.Price;

                await _context.SaveChangesAsync();
            }

            return _product;
        }
        public async Task<List<Product>> GetProductsByCategory(int idCategory)
        {
            return  await _context.Products.Where(c => c.CategoryId == idCategory).ToListAsync();
        }

        public async Task<Product> AddCommentToProduct(Comment comment)
        {
            var product = await _context.Products.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == comment.ProductId);

            product.Comments.Add(comment);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> AddImagesToProduct(List<Image> images)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == images.First().ProductId);

            foreach (var image in images)
            {
                product.Images.Add(image);
            }

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteCommentToProduct(Comment comment)
        {
            var product = await _context.Products.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == comment.ProductId);
            var _comment = product.Comments.Where(c => c.Id == comment.Id).FirstOrDefault();

            if (_comment != null)
            {
                _context.Comments.Remove(_comment);
                await _context.SaveChangesAsync();
                return product;
            }

            return null;
        }

        public async Task<Product> DeleteImageToProduct(Image image)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == image.ProductId);
            var _image = product.Images.Where(c => c.Id == image.Id).FirstOrDefault();

            if (_image != null)
            {
                _context.Images.Remove(_image);
                await _context.SaveChangesAsync();
                return product;
            }

            return null;
        }
    }
}
