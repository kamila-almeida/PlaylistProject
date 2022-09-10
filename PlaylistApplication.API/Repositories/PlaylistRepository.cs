using Dapper;
using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Repositories.Interfaces;
using System.Data.SqlClient;

namespace PlaylistApplication.API.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly SqlConnection _connection;

        public PlaylistRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> CreateAsync(Playlist playlist, CancellationToken cancellationToken)
        {
            string query = "INSERT INTO Playlist OUTPUT Inserted.Id VALUES (@Name, @Description)";

            var commandDefinition = new CommandDefinition(query, new { Name = playlist.Name, Description = playlist.Description }, cancellationToken: cancellationToken);

            return await _connection.QueryFirstAsync<int>(commandDefinition);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            string query = "DELETE Playlist WHERE Id = @Id";

            var commandDefinition = new CommandDefinition(query, new { Id = id }, cancellationToken: cancellationToken);

            await _connection.ExecuteAsync(commandDefinition);
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync(CancellationToken cancellationToken)
        {
            string query = "SELECT * FROM Playlist";

            var commandDefinition = new CommandDefinition(query, cancellationToken: cancellationToken);

            return await _connection.QueryAsync<Playlist>(commandDefinition);
        }

        public async Task<Playlist> GetOneAsync(int id, CancellationToken cancellationToken)
        {
            string query = "SELECT * FROM Playlist WHERE Id = @Id";

            var commandDefinition = new CommandDefinition(query, new { Id = id }, cancellationToken: cancellationToken);

            return await _connection.QueryFirstOrDefaultAsync<Playlist>(commandDefinition);
        }

        public async Task<bool> UpdateAsync(Playlist playlist, CancellationToken cancellationToken)
        {
            string query = "UPDATE Playlist SET Name = @Name, Description = @Description WHERE Id = @Id";

            var commandDefinition = new CommandDefinition(query, new { Name = playlist.Name, Description = playlist.Description, Id = playlist.Id }, cancellationToken: cancellationToken);

            return await _connection.ExecuteAsync(commandDefinition) > 0;
        }

        public async Task<IEnumerable<Song>> GetSongsByPlaylistAsync(int id, CancellationToken cancellationToken)
        {
            string query = @"SELECT * FROM Song s
            LEFT JOIN SongsPlaylists sp ON (s.Id = sp.SongId)    
            WHERE sp.PlaylistId = @PlaylistId";

            var commandDefinition = new CommandDefinition(query, new { PlaylistId = id }, cancellationToken: cancellationToken);

            return await _connection.QueryAsync<Song>(commandDefinition);
        }        
    }
}
