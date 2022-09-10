using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Validators;

namespace PlaylistApplication.UnitTests.EntititesValidation
{
    public class SongValidationTest
    {
        private readonly SongValidator _songValidator;

        public SongValidationTest()
        {
            _songValidator = new SongValidator();
        }

        [Fact]
        public async Task Validation_SongPropertiesHaveValidValues_ValidationShouldReturnTrue()
        {
            // Arrange
            var song = new Song
            {
                Name = "Smooth Criminal",
                Author = "Michael Jackson"
            };

            // Act
            var result = _songValidator.Validate(song);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Validation_SongPropertiesHaveInvalidValues_ValidationShouldReturnTrue()
        {
            // Arrange
            var song = new Song
            {
                Name = "",
                Author = null
            };

            // Act
            var result = _songValidator.Validate(song);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorMessage.Equals("Name is required."));
            Assert.Contains(result.Errors, x => x.ErrorMessage.Equals("Author is required."));
        }
    }
}
