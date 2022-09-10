using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Services.Interfaces
{
    public interface ISongService
    {
        Task<Song> GetOneAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Song>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> CreateAsync(Song song, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Song song, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Playlist>> GetPlaylistsBySongAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken);
        Task DeleteSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken);
    }
}
