﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
