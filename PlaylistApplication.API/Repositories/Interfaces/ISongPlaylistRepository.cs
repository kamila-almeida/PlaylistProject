using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Repositories.Interfaces
{
    public interface ISongPlaylistRepository
    {
        Task<bool> CreateSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken);
        Task DeleteSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken);
        Task DeleteSongPlaylistBySongAsync(int songId, CancellationToken cancellationToken);
        Task DeleteSongPlaylistByPlaylistAsync(int playlistId, CancellationToken cancellationToken);
    }
}
