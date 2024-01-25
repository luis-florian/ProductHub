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
    public class ProductService(DBContext context) : IProductService
    {
        private readonly DBContext _context = context;

        public async Task<Product?> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<Product?> GetById(int id)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(c => c.Images)
                .Include(c => c.Comments!)
                    .ThenInclude(c => c.User)
                .SingleOrDefaultAsync(p => p.Id == id);

            return product;
        }
        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .Include(c => c.Category)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Image = p.Images!.FirstOrDefault()
                })
                .ToListAsync();
        }
        public async Task<Product?> Update(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);

            if (existingProduct is not null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Stock = product.Stock;
                existingProduct.Price = product.Price;

                await _context.SaveChangesAsync();
            }

            return existingProduct;
        }
        public async Task<List<Product>> GetProductsByCategory(int idCategory)
        {
            return await _context.Products
                .Where(c => c.CategoryId == idCategory)
                .Include(c => c.Category)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Image = p.Images!.FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<Product?> AddCommentToProduct(Comment comment)
        {
            var product = await _context.Products.FindAsync(comment.ProductId);

            if (product != null)
            {
                product.Comments ??= new List<Comment>();

                product.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }

            return product;
        }

        public async Task<Product?> AddImagesToProduct(List<Image> images)
        {
            var product = await _context.Products.FindAsync(images.First().ProductId);

            if (product != null)
            {
                product.Images ??= new List<Image>();

                foreach (var image in images)
                {
                    product.Images.Add(image);
                }

                await _context.SaveChangesAsync();
            }

            return product;
        }

        public async Task<Product?> DeleteCommentToProduct(Comment comment)
        {
            var product = await _context.Products.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == comment.ProductId);

            if (product != null)
            {
                var commentToRemove = product.Comments!.FirstOrDefault(c => c.Id == comment.Id);

                if (commentToRemove != null)
                {
                    _context.Comments.Remove(commentToRemove);
                    await _context.SaveChangesAsync();

                    return product;
                }

                return null;
            }

            return null;
        }

        public async Task<Product?> DeleteImageToProduct(Image image)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == image.ProductId);

            if (product != null)
            {
                var imageToRemove = product.Images!.FirstOrDefault(c => c.Id == image.Id);

                if (imageToRemove != null)
                {
                    _context.Images.Remove(imageToRemove);
                    await _context.SaveChangesAsync();

                    return product;
                }

                return null;
            }

            return null;
        }

        public async Task<Image?> GetImageFromProduct(int idProduct, int idImage)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == idProduct);

            if (product != null)
            {
                var image = product.Images!.FirstOrDefault(img => img.Id == idImage);
                return image;
            }

            return null;
        }
    }
}
