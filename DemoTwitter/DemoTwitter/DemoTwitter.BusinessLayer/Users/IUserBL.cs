﻿using System.Collections.Generic;
using DemoTwitter.Models;

namespace DemoTwitter.BusinessLayer.Users
{
    public interface IUserBL
    {

        void Register(User user);
        void Remove(User user);
        void Update(User updatedUser);
        User GetByUsername(string userName);
        IEnumerable<User> GetAllUsers();
    }
}
