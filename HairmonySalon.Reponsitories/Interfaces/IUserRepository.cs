using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonySalon.Reponsitories.Entities;

namespace Harmony.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<List<User>> GetAllUser();

	}
}
