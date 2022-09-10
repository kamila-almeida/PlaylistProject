using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<Song> GetOneAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Song>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> CreateAsync(Song song, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Song song, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Playlist>> GetPlaylistsBySongAsync(int id, CancellationToken cancellationToken);
    }
}
