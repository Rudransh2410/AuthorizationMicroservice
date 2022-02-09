using AuthorizationService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationService.Repository
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        private IConfiguration configuration;

        private readonly IMongoCollection<User> _users;
        public AuthenticationRepository(IConfiguration configuration,IDbClient dbClient)
        {
            this.configuration = configuration;

            _users = dbClient.GetTweetCollection();

        }
        public User Authentication(string email, string password)
        {

            var user = _users.Find(u => u.Email == email && u.Password == password).FirstOrDefault(); ;
           
            return user;
        }
        public string TokenGeneration(string email, string password)
        {
     


            //check if user exist using the method Authentication() in JwtAuthenticationRepository
            User user = Authentication(email, password);

            if (user == null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwtoken:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
                new Claim("Id", user.Id)
            };


            var token = new JwtSecurityToken(
                        issuer: configuration["Jwtoken:Issuer"],
                        audience: configuration["Jwtoken:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddSeconds(15*60),
                        signingCredentials: credentials);
       
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public User GetUserById(string userId)
        {
            var filter = Builders<User>.Filter.Eq("Id", userId);

            var user = _users.Find(filter).FirstOrDefault();

            return user;
        }
        public User GetUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);

            var user = _users.Find(filter).FirstOrDefault();

            return user;
        }
        public List<User> GetUsers()
        {
            return _users.Find(user => true).ToList();
        }
        public User AddUser(User user)
        {
            _users.InsertOne(user);

            return user;
        }
        public bool ChangePassword(string email, string password)
        {
            User user = GetUserByEmail(email);

            if(user == null)
            {
                return false;
            }
            var filter = Builders<User>.Filter.Eq("Email", email);
            user.Password = password;
            user.ConfirmPassword = password;
            _users.ReplaceOne(filter, user);
            return true;
        }

    }
}
