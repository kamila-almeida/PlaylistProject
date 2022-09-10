using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Repositories.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<Playlist> GetOneAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Playlist>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> CreateAsync(Playlist playlist, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Playlist playlist, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Song>> GetSongsByPlaylistAsync(int id, CancellationToken cancellationToken);
    }
}
