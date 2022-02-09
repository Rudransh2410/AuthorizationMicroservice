using AuthorizationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Repository
{
    public interface IAuthenticationService
    {
        User Authentication(string name, string password);
        string TokenGeneration(string name, string password);
        User AddUser(User user);
        User GetUserById(string userId);
        User GetUserByEmail(string email);
        List<User> GetUsers();
        bool ChangePassword(string email, string password);
    }
}
