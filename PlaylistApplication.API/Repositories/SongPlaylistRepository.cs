using Dapper;
using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Repositories.Interfaces;
using System.Data.SqlClient;

namespace PlaylistApplication.API.Repositories
{
    public class SongPlaylistRepository : ISongPlaylistRepository
    {
        private readonly SqlConnection _connection;

        public SongPlaylistRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> CreateSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken)
        {
            string query = "INSERT SongsPlaylists VALUES (@SongId, @PlaylistId)";

            var commandDefinition = new CommandDefinition(query, new { SongId = songPlaylist.SongId, PlaylistId = songPlaylist.PlaylistId }, cancellationToken: cancellationToken);

            return await _connection.ExecuteAsync(commandDefinition) > 0;
        }

        public async Task DeleteSongPlaylistByPlaylistAsync(int playlistId, CancellationToken cancellationToken)
        {
            string query = "DELETE SongsPlaylists WHERE PlaylistId = @PlaylistId";

            var commandDefinition = new CommandDefinition(query, new { PlaylistId = playlistId }, cancellationToken: cancellationToken);

            await _connection.ExecuteAsync(commandDefinition);
        }

        public async Task DeleteSongPlaylistBySongAsync(int songId, CancellationToken cancellationToken)
        {
            string query = "DELETE SongsPlaylists WHERE SongId = @SongId";

            var commandDefinition = new CommandDefinition(query, new { SongId = songId }, cancellationToken: cancellationToken);

            await _connection.ExecuteAsync(commandDefinition);
        }

        public async Task DeleteSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken)
        {
            string query = "DELETE SongsPlaylists WHERE SongId = @SongId AND PlaylistId = @PlaylistId";

            var commandDefinition = new CommandDefinition(query, new { SongId = songPlaylist.SongId, PlaylistId = songPlaylist.PlaylistId }, cancellationToken: cancellationToken);

            await _connection.ExecuteAsync(commandDefinition);
        }
    }
}
