using Moq;
using PlaylistApplication.UnitTests.Fixtures;

namespace PlaylistApplication.UnitTests.Services.Song
{
    public class SongServiceTest : IClassFixture<SongServiceFixture>
    {
        private readonly SongServiceFixture _fixture;
        public SongServiceTest(SongServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetOne_Song_ReturnsSong()
        {
            _fixture._songRepository.Setup(x =>
                x.GetOneAsync(1, CancellationToken.None)).ReturnsAsync(() => _fixture.songs.Where(x => x.Id == 1).First());

            var result = await _fixture._songService.GetOneAsync(1, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result?.Id.Equals(1));
            Assert.True(result?.Name.Equals("Toxic"));
            Assert.True(result?.Author.Equals("Britney Spears"));
        }

        [Fact]
        public async Task GetAll_Song_ReturnsSong()
        {
            _fixture._songRepository.Setup(x =>
                x.GetAllAsync(CancellationToken.None)).ReturnsAsync(() => _fixture.songs);

            var result = await _fixture._songService.GetAllAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Count() > 1);
        }

        [Fact]
        public async Task Get_SongPlaylist_ReturnsPlaylists()
        {
            _fixture._songRepository.Setup(x =>
                x.GetPlaylistsBySongAsync(3, CancellationToken.None)).ReturnsAsync(() => (_fixture.playlists.Where(p =>
                    _fixture.songsPlaylists.Where(sp => sp.SongId == 3).Select(sp => sp.PlaylistId).Contains(p.Id)
                )));

            var result = await _fixture._songService.GetPlaylistsBySongAsync(3, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task Create_Song_ReturnsId()
        {
            var song = new API.Entities.Song()
            {
                Id = 4,
                Name = "New Rules",
                Author = "Dua Lipa"
            };

            _fixture._songRepository.Setup(x =>
               x.CreateAsync(song, CancellationToken.None)).ReturnsAsync(() => song.Id);

            var result = await _fixture._songService.CreateAsync(song, CancellationToken.None);

            Assert.True(result.Equals(4));
        }

        [Fact]
        public async Task Update_Song_ReturnsTrue()
        {
            var song = new API.Entities.Song()
            {
                Id = 4,
                Name = "New Rules",
                Author = "Dua Lipa"
            };

            _fixture._songRepository.Setup(x =>
               x.UpdateAsync(song, CancellationToken.None)).ReturnsAsync(() => true);

            var result = await _fixture._songService.UpdateAsync(song, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task Delete_Song_ShouldNotReturnException()
        {
            try
            {
                await _fixture._songService.DeleteAsync(1, CancellationToken.None);
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

            var result = await _fixture._songService.CreateSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None);

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

            var result = await _fixture._songService.CreateSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None);

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
                _fixture._songService.DeleteSongPlaylistRelationshipAsync(songPlaylist, CancellationToken.None);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}
