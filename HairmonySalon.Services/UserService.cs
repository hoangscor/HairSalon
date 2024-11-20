using Harmony.Repositories.Interfaces;
using Harmony.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony.Repositories.Entities;
namespace Harmony.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repository;

		public UserService(IUserRepository repository)
		{
			_repository = repository;
		}

		public Task<List<User>> Users()
		{
			return _repository.GetAllUser();
		}
        // Thêm logic xác thực
        public Task<User?> AuthenticateUser(string email, string password)
        {
            return _repository.AuthenticateUser(email, password);
        }
    }
}
