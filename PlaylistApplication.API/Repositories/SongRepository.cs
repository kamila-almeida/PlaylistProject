using Dapper;
using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Repositories.Interfaces;
using System.Data.SqlClient;
using System.Threading;

namespace PlaylistApplication.API.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly SqlConnection _connection;

        public SongRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> CreateAsync(Song song, CancellationToken cancellationToken)
        {
            string query = "INSERT INTO Song OUTPUT Inserted.Id VALUES (@Name, @Author)";

            var commandDefinition = new CommandDefinition(query, new { Name = song.Name, Author = song.Author }, cancellationToken: cancellationToken);

            return await _connection.QueryFirstAsync<int>(commandDefinition);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            string query = "DELETE Song WHERE Id = @Id";

            var commandDefinition = new CommandDefinition(query, new { Id = id }, cancellationToken: cancellationToken);

            await _connection.ExecuteAsync(commandDefinition);
        }

        public async Task<IEnumerable<Song>> GetAllAsync(CancellationToken cancellationToken)
        {
            string query = "SELECT * FROM Song";

            var commandDefinition = new CommandDefinition(query, cancellationToken: cancellationToken);

            return await _connection.QueryAsync<Song>(commandDefinition);
        }

        public async Task<Song> GetOneAsync(int id, CancellationToken cancellationToken)
        {
            string query = "SELECT * FROM Song WHERE Id = @Id";

            var commandDefinition = new CommandDefinition(query, new { Id = id }, cancellationToken: cancellationToken);

            return await _connection.QueryFirstOrDefaultAsync<Song>(commandDefinition);
        }

        public async Task<bool> UpdateAsync(Song song, CancellationToken cancellationToken)
        {
            string query = "UPDATE Song SET Name = @Name, Author = @Author WHERE Id = @Id";

            var commandDefinition = new CommandDefinition(query, new { Name = song.Name, Author = song.Author, Id = song.Id }, cancellationToken: cancellationToken);

            return await _connection.ExecuteAsync(commandDefinition) > 0;
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsBySongAsync(int id, CancellationToken cancellationToken)
        {
            string query = @"SELECT * FROM Playlist p
            LEFT JOIN SongsPlaylists sp ON (p.Id = sp.PlaylistId)    
            WHERE sp.SongId = @SongId"
            ;

            var commandDefinition = new CommandDefinition(query, new { SongId = id }, cancellationToken: cancellationToken);

            return await _connection.QueryAsync<Playlist>(commandDefinition);
        }        
    }
}
