using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Validators;

namespace PlaylistApplication.UnitTests.EntititesValidation
{
    public class PlaylistValidationTest
    {
        private readonly PlaylistValidator _playlistValidator;

        public PlaylistValidationTest()
        {
            _playlistValidator = new PlaylistValidator();
        }

        [Fact]
        public async Task Validation_PlaylistPropertiesHaveValidValues_ValidationShouldReturnTrue()
        {
            // Arrange
            var playlist = new Playlist
            {
                Name = "80s classics",
                Description = "Nostalgic songs"
            };

            // Act
            var result = _playlistValidator.Validate(playlist);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Validation_PlaylistPropertiesHaveInvalidValues_ValidationShouldReturnTrue()
        {
            // Arrange
            var playlist = new Playlist
            {
                Name = "",
                Description = null
            };

            // Act
            var result = _playlistValidator.Validate(playlist);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorMessage.Equals("Name is required."));
            Assert.Contains(result.Errors, x => x.ErrorMessage.Equals("Description is required."));
        }
    }
}
