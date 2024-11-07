using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony.Repositories.Interfaces;
using HarmonySalon.Reponsitories.Entities;

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
	}
}
