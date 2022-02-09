using AuthorizationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Repository
{
    public class AuthenticationService:IAuthenticationService
    {
        private static IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        //Check if user exist
        public User Authentication(string email, string password)
        {
            return _authenticationRepository.Authentication(email, password);
        }
        public User GetUserById(string userId)
        {
            return _authenticationRepository.GetUserById(userId);
        }
        public string TokenGeneration(string email, string password)
        {
            return _authenticationRepository.TokenGeneration(email, password);
        }
        public List<User> GetUsers()
        {
            return _authenticationRepository.GetUsers();
        }
        public User GetUserByEmail(string email)
        {
            return _authenticationRepository.GetUserByEmail(email);
        }
        public User AddUser(User user)
        {
            return _authenticationRepository.AddUser(user);
        }
        public bool ChangePassword(string email, string password)
        {
            return _authenticationRepository.ChangePassword(email, password);
        }
    }
}
