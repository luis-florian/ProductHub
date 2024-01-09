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
        public Task<Product> AddCommentToProduct(int id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Product> AddImagesToProduct(int id, List<Image> images)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteCommentToProduct(int id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteImagesToProduct(int id, List<Image> images)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductsByCategory(int idCategory)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
