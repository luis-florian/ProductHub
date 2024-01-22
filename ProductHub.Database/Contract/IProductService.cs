﻿using ProductHub.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Contract
{
    public interface IProductService
    {
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Get(int id);
        Task<List<Product>> Get();
        Task<List<Product>> GetProductsByCategory(int idCategory);
        Task<Product> AddImagesToProduct(List<Image> images);
        Task<Product> DeleteImageToProduct(Image image);
        Task<Product> AddCommentToProduct(Comment comment);
        Task<Product> DeleteCommentToProduct(Comment comments);
    }
}
