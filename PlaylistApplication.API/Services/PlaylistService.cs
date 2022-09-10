using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Repositories.Interfaces;
using PlaylistApplication.API.Services.Interfaces;

namespace PlaylistApplication.API.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISongRepository _songRepository;
        private readonly ISongPlaylistRepository _songPlaylistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository, ISongRepository songRepository,ISongPlaylistRepository songPlaylistRepository)
        {
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
            _songPlaylistRepository = songPlaylistRepository;
        }

        public async Task<int> CreateAsync(Playlist playlist, CancellationToken cancellationToken)
        {
            return await _playlistRepository.CreateAsync(playlist, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _songPlaylistRepository.DeleteSongPlaylistByPlaylistAsync(id, cancellationToken);
            await _playlistRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<Playlist> GetOneAsync(int id, CancellationToken cancellationToken)
        {
            return await _playlistRepository.GetOneAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _playlistRepository.GetAllAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Playlist playlist, CancellationToken cancellationToken)
        {
            return await _playlistRepository.UpdateAsync(playlist, cancellationToken);
        }

        public async Task<IEnumerable<Song>> GetSongsByPlaylistAsync(int id, CancellationToken cancellationToken)
        {
            return await _playlistRepository.GetSongsByPlaylistAsync(id, cancellationToken);
        }

        public async Task<bool> CreateSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken)
        {
            var songExists = (await _songRepository.GetOneAsync(songPlaylist.SongId, cancellationToken))?.Id > 0;
            var playlistExists = (await _playlistRepository.GetOneAsync(songPlaylist.PlaylistId, cancellationToken))?.Id > 0;

            if (songExists && playlistExists)
                return await _songPlaylistRepository.CreateSongPlaylistRelationshipAsync(songPlaylist, cancellationToken);
            else
                return false;
        }

        public async Task DeleteSongPlaylistRelationshipAsync(SongPlaylist songPlaylist, CancellationToken cancellationToken)
        {
            await _songPlaylistRepository.DeleteSongPlaylistRelationshipAsync(songPlaylist, cancellationToken);
        }
    }
}
