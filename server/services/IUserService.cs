using System;
using System.Collections.Generic;
using MongoDB.Driver;
using server.Models;
using server.Helpers;

namespace server.Services
{
    public interface IUserService
    {
        IList<User> Read();
        User Create(User user);
        void Update(User user);
        User Find(string id);
        void Delete(string id);
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}