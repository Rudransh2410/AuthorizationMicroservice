using AuthorizationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Repository
{
    public interface IAuthenticationRepository
    {
        User Authentication(string email, string password);
        string TokenGeneration(string email, string password);
        User AddUser(User user);
        User GetUserById(string userId);
        User GetUserByEmail(string email);
        List<User> GetUsers();
        bool ChangePassword(string email,string password);


    }
}
