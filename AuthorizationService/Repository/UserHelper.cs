using AuthorizationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Repository
{
    public class UserHelper
    {
        public static List<User> users = new List<User>
        {
            new User { Id = "61d16eb7a65a2b95c273f879", Image = "is", FirstName = "Mathew", LastName = "Thomas", 
                Email = "14235@gmail.com", ContactNumber = "1234566557",Password="123pass",ConfirmPassword="123pass" },
            new User{ Id="61d16eb7a65a2b95c273e456",Image="is",FirstName="Alex",LastName="Jacob",
                Email="123@gmail.com",ContactNumber="12345667"},
        };  
    }
}
