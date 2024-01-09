using ProductHub.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Contract
{
    public interface ICategoryService
    {
        Task<Category?> Create(Category category);
        Task<Category?> Update(Category category);
        Task<Category?> Get(int id);
        Task<List<Category?>> Get();
    }
}
