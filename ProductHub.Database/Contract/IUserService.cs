using ProductHub.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Contract
{
    public interface IUserService
    {
        Task<User?> Create(User user);
        Task<User?> Update(User user);
        Task<User?> Get(int id);
        Task<List<User?>> Get();
    }
}
