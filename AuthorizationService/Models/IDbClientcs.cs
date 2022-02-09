using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Models
{
   public interface IDbClient
    {
        IMongoCollection<User> GetTweetCollection();
    }
}
