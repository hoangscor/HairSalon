﻿using HarmonySalon.Reponsitories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmony.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> Users();
    }
}
