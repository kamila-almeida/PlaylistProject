using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Repositories.Interfaces;
using PlaylistApplication.API.Services.Interfaces;

namespace PlaylistApplication.API.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISongPlaylistRepository _songPlaylistRepository;

        public SongService(ISongRepository songRepository, IPlaylistRepository playlistRepository, ISongPlaylistRepository songPlaylistRepository)
        {
            _songRepository = songRepository;
            _playlistRepository = playlistRepository;
            _songPlaylistRepository = songPlaylistRepository;
        }

        public async Task<int> CreateAsync(Song song, CancellationToken cancellationToken)
        {
            return await _songRepository.CreateAsync(song, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _songPlaylistRepository.DeleteSongPlaylistBySongAsync(id, cancellationToken);
            await _songRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<Song> GetOneAsync(int id, CancellationToken cancellationToken)
        {
            return await _songRepository.GetOneAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Song>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _songRepository.GetAllAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Song song, CancellationToken cancellationToken)
        {
            return await _songRepository.UpdateAsync(song, cancellationToken);
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsBySongAsync(int id, CancellationToken cancellationToken)
        {
            return await _songRepository.GetPlaylistsBySongAsync(id, cancellationToken);
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
