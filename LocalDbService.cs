using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GroupNineMobileProject
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Profile>();
            _connection.CreateTableAsync<LoggedGames>();
        }

        //Logging profile methods
        public async Task<List<Profile>> GetProfiles()
        {
            return await _connection.Table<Profile>().ToListAsync();
        }

        public async Task<Profile> GetByEmail(string email)
        {
            return await _connection.Table<Profile>().Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByCredentials(string username, string password)
        {
            return await _connection.Table<Profile>()
                .Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task Create(Profile profile)
        {
            await _connection.InsertAsync(profile);
        }

        public async Task Delete(Profile profile)
        {
            await _connection.DeleteAsync(profile);
        }

        //Logging game methods
        public async Task LogGame(LoggedGames game)
        {
            await _connection.InsertAsync(game);
        }

        public async Task<List<LoggedGames>> GetLoggedGames(int profileId)
        {
            return await _connection.Table<LoggedGames>()
                .Where(g => g.ProfileId == profileId)
                .ToListAsync();
        }
    }
}
