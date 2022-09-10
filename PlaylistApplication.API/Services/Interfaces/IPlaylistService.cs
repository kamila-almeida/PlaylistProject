using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Services.Interfaces
{
    public interface IPlaylistService
    {
        Task<Playlist> GetOneAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Playlist>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> CreateAsync(Playlist playlist, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Playlist playlist, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Song>> GetSongsByPlaylistAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken);
        Task DeleteSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken);
    }
}
