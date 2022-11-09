using Authentication_JWT_Net5.Models;
using System.Threading.Tasks;

namespace Authentication_JWT_Net5.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username, string password);
    }
}
