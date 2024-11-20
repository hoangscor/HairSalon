using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony.Repositories.Entities;

namespace Harmony.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser();
        Task<User?> AuthenticateUser(string email, string password); // Thêm phương thức xác thực
    }
}
