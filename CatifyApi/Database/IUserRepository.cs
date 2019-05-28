using CatifyApi.Database.Models;
using System.Threading.Tasks;

namespace CatifyApi.Database
{
    public interface IUserRepository
    {
        Task<User> GetAvailableUser();

        Task Add(User user);

        Task Park(int userId);

        Task Unpark(int userId);
    }
}
