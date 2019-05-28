using CatifyApi.Database.Models;
using SQLite;
using System.Threading.Tasks;

namespace CatifyApi.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly string _dbPath;
        private readonly SQLiteAsyncConnection _db;

        public UserRepository()
        {
            _dbPath = "User.db";
            _db = new SQLiteAsyncConnection(_dbPath);
            _db.CreateTableAsync<User>();
        }

        public async Task<User> GetAvailableUser()
        {
            return await _db.FindAsync<User>(x => x.Available);
        }

        public async Task Add(User user)
        {
            await _db.InsertAsync(user);
        }

        public async Task Park(int userId)
        {
            var user = await _db.GetAsync<User>(userId);
            user.Available = false;

            await _db.UpdateAsync(user);
        }

        public async Task Unpark(int userId)
        {
            var user = await _db.GetAsync<User>(userId);
            user.Available = true;

            await _db.UpdateAsync(user);
        }
    }
}
