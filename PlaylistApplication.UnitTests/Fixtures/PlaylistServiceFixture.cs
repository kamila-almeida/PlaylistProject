using Moq;
using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Repositories.Interfaces;
using PlaylistApplication.API.Services;
using PlaylistApplication.API.Services.Interfaces;

namespace PlaylistApplication.UnitTests.Fixtures
{
    public class PlaylistServiceFixture
    {
        public IPlaylistService _playlistService { get; set; }
        public Mock<IPlaylistRepository> _playlistRepository { get; set; }
        public Mock<ISongRepository> _songRepository { get; set; }
        public Mock<ISongPlaylistRepository> _songPlaylistRepository { get; set; }

        public List<Song> songs = new List<Song>()
        {
            new Song()
            {
                Id = 1,
                Name = "Toxic",
                Author = "Britney Spears"
            },
            new Song()
            {
                Id = 2,
                Name = "Decode",
                Author = "Paramore"
            },
            new Song()
            {
                Id = 3,
                Name = "Numb",
                Author = "Linkin Park"
            }
        };

        public List<Playlist> playlists = new List<Playlist>()
        {
            new Playlist()
            {
                Id = 1,
                Name = "Pop",
                Description = "Best of pop music"
            },
            new Playlist()
            {
                Id = 2,
                Name = "Rock",
                Description = "Some rock songs"
            },
            new Playlist()
            {
                Id = 3,
                Name = "Nu-Metal",
                Description = "Classics of nu metal"
            }
        };

        public List<SongPlaylist> songsPlaylists = new List<SongPlaylist>()
        {
            new SongPlaylist()
            {
                SongId = 1,
                PlaylistId = 1
            },
            new SongPlaylist()
            {
                SongId = 2,
                PlaylistId = 2
            },
            new SongPlaylist()
            {
                SongId = 3,
                PlaylistId = 2
            },
            new SongPlaylist()
            {
                SongId = 3,
                PlaylistId = 3
            }
        };        

        public PlaylistServiceFixture()
        {
            _playlistRepository = new Mock<IPlaylistRepository>();
            _songRepository = new Mock<ISongRepository>();
            _songPlaylistRepository = new Mock<ISongPlaylistRepository>();
            _playlistService = new PlaylistService(_playlistRepository.Object, _songRepository.Object, _songPlaylistRepository.Object);
        }
    }
}
