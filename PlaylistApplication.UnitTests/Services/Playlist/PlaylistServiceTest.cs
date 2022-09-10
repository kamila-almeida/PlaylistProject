using Moq;
using PlaylistApplication.UnitTests.Fixtures;

namespace PlaylistApplication.UnitTests.Services.Playlist
{
    public class PlaylistServiceTest : IClassFixture<PlaylistServiceFixture>
    {
        private readonly PlaylistServiceFixture _fixture;
        public PlaylistServiceTest(PlaylistServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetOne_Playlist_ReturnsPlaylist()
        {
            _fixture._playlistRepository.Setup(x =>
                x.GetOneAsync(1, CancellationToken.None)).ReturnsAsync(() => _fixture.playlists.Where(x => x.Id == 1).First());

            var result = await _fixture._playlistService.GetOneAsync(1, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result?.Id.Equals(1));
            Assert.True(result?.Name.Equals("Pop"));
            Assert.True(result?.Description.Equals("Best of pop music"));
        }

        [Fact]
        public async Task GetAll_Playlist_ReturnsPlaylists()
        {
            _fixture._playlistRepository.Setup(x =>
                x.GetAllAsync(CancellationToken.None)).ReturnsAsync(() => _fixture.playlists);

            var result = await _fixture._playlistService.GetAllAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Count() > 1);
        }

        [Fact]
        public async Task Get_SongPlaylist_ReturnsSongs()
        {
            _fixture._playlistRepository.Setup(x =>
                x.GetSongsByPlaylistAsync(2, CancellationToken.None)).ReturnsAsync(() => (_fixture.songs.Where(s =>
                    _fixture.songsPlaylists.Where(sp => sp.PlaylistId == 2).Select(sp => sp.SongId).Contains(s.Id)
                )));

            var result = await _fixture._playlistService.GetSongsByPlaylistAsync(2, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task Create_Playlist_ReturnsId()
        {
            var playlist = new API.Entities.Playlist()
            {
                Id = 4,
                Name = "King of pop",
                Description = "Michael Jackson best songs"
            };

            _fixture._playlistRepository.Setup(x =>
               x.CreateAsync(playlist, CancellationToken.None)).ReturnsAsync(() => playlist.Id);

            var result = await _fixture._playlistService.CreateAsync(playlist, CancellationToken.None);

            Assert.True(result.Equals(4));
        }

        [Fact]
        public async Task Update_Playlist_ReturnsTrue()
        {
            var playlist = new API.Entities.Playlist()
            {
                Id = 4,
                Name = "King of pop",
                Description = "Michael Jackson best songs"
            };

            _fixture._playlistRepository.Setup(x =>
               x.UpdateAsync(playlist, CancellationToken.None)).ReturnsAsync(() => true);

            var result = await _fixture._playlistService.UpdateAsync(playlist, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task Delete_Playlist_ShouldNotReturnException()
        {
            try
            {
                await _fixture._playlistService.DeleteAsync(1, CancellationToken.None);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task Create_SongPlaylist_ReturnsTrue()
        {
            var songPlaylist = new API.Entities.SongPlaylist()
            {
                SongId = 1,
                PlaylistId = 2
            };

            _fixture._songRepository.Setup(x =>
            x.GetOneAsync(songPlaylist.SongId, CancellationToken.None)).ReturnsAsync(() => _fixture.songs.Where(x => x.Id == songPlaylist.SongId).First());

            _fixture._playlistRepository.Setup(x =>
                x.GetOneAsync(songPlaylist.PlaylistId, CancellationToken.None)).ReturnsAsync(() => _fixture.playlists.Where(x => x.Id == songPlaylist.PlaylistId).First());

            _fixture._songPlaylistRepository.Setup(x =>
               x.CreateSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None)).ReturnsAsync(() => true);

            var result = await _fixture._playlistService.CreateSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task Create_SongPlaylist_ReturnsFalse()
        {
            var songPlaylist = new API.Entities.SongPlaylist()
            {
                SongId = 4,
                PlaylistId = 2
            };

            _fixture._songRepository.Setup(x =>
            x.GetOneAsync(songPlaylist.SongId, CancellationToken.None)).ReturnsAsync(() => _fixture.songs.Where(x => x.Id == songPlaylist.SongId).FirstOrDefault());

            _fixture._playlistRepository.Setup(x =>
                x.GetOneAsync(songPlaylist.PlaylistId, CancellationToken.None)).ReturnsAsync(() => _fixture.playlists.Where(x => x.Id == songPlaylist.PlaylistId).First());

            _fixture._songPlaylistRepository.Setup(x =>
               x.CreateSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None)).ReturnsAsync(() => true);

            var result = await _fixture._playlistService.CreateSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public void Delete_SongPlaylist_ShouldNotReturnException()
        {
            var songPlaylist = new API.Entities.SongPlaylist()
            {
                SongId = 1,
                PlaylistId = 2
            };

            try
            {
                _fixture._playlistService.DeleteSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}
