using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using server.Models;
using server.Helpers;

namespace server.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IAppSettings _appSettings;

        public UserService(IDatabaseSettings settings, IAppSettings appSettings)
        {
            try
            {
                _appSettings = appSettings;
                //db
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);
                _users = database.GetCollection<User>("users");
            }
            catch (MongoException ex)
            {
                throw ex;
            }
        }

        public User Create(User _user)
        {
            var _userEmailExist = _users.Find(user => user.UserName == _user.UserName).SingleOrDefault();
            if (_userEmailExist != null) throw new Exception("Username already exist");

            _users.InsertOne(_user);
            return _user;
        }

        public void Update(User user) =>
            _users.ReplaceOne(_user => _user.Id == user.Id, user);


        public IList<User> Read() =>
            _users.Find(user => true).ToList();

        public User Find(string id) =>
            _users.Find(user => user.Id == id).SingleOrDefault();

        public void Delete(string id) =>
            _users.DeleteOne(user => user.Id == id);

        public User Authenticate(string username, string password)
        {
            var user = _users.Find(user => true).ToList().SingleOrDefault(x => x.UserName == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Find(user => true).ToList().WithoutPasswords();
        }
    }
}