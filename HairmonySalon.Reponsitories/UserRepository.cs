using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony.Repositories.Interfaces;
using Harmony.Repositories.Entities;

namespace Harmony.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HarmonySalonContext _dbContext;

        public UserRepository(HarmonySalonContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _dbContext.Users.ToListAsync();
        }
        // Thêm phương thức xác thực người dùng
        public async Task<User?> AuthenticateUser(string email, string password)
        {
            // Tìm người dùng theo email và mật khẩu
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }

}
