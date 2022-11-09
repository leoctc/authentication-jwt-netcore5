using Authentication_JWT_Net5.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication_JWT_Net5.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUser(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "batman", Password = "123", Role = "manager" },
                new User { Id = 2, Username = "leonardo", Password = "123", Role = "employee" }
            };

            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password.ToLower()).FirstOrDefault();
        }       
    }
}
