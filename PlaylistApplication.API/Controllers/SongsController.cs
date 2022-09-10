using Microsoft.AspNetCore.Mvc;
using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Services.Interfaces;
using PlaylistApplication.API.Validators;
using System.Net;

namespace PlaylistApplication.API.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ISongService _songService;
        private readonly SongValidator _songValidator;

        public SongsController(ILogger<SongsController> logger, ISongService songService, SongValidator songValidator)
        {
            _logger = logger;
            _songService = songService;
            _songValidator = songValidator;
        }

        /// <summary>
        /// Get a song by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns the song by the requested id</response>
        /// <response code="400">If the given id is equal or less than 0</response>
        /// <response code="404">If no song is found for the requested id</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0) return BadRequest();

                var result = await _songService.GetOneAsync(id, cancellationToken);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get all the songs
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns all the songs</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _songService.GetAllAsync(cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create a song
        /// </summary>
        /// <param name="song"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns the id of the song created</response>
        /// <response code="400">If the song values are invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Song song, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = _songValidator.Validate(song);
                if (validationResult.IsValid)
                {
                    var result = await _songService.CreateAsync(song, cancellationToken);
                    return Ok(result);
                }

                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update a song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="song"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">If the song is updated</response>
        /// <response code="400">If the song values are invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Song song, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0 || id != song.Id) return BadRequest();

                var validationResult = _songValidator.Validate(song);
                if (validationResult.IsValid)
                {
                    var result = await _songService.UpdateAsync(song, cancellationToken);
                    return Ok();
                }

                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete a song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">If the song is deleted</response>
        /// <response code="400">If the given id is equal or less than 0</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0) return BadRequest();

                await _songService.DeleteAsync(id, cancellationToken);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create a relationship between song and playlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songPlaylist"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">if the relationship is created</response>
        /// <response code="400">If the given id is equal or less than 0</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost("{id}/playlists")]
        public async Task<IActionResult> CreateSongPlaylistRelationship(int id, [FromBody] SongPlaylist songPlaylist, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0 || id != songPlaylist.SongId) return BadRequest();

                var result = await _songService.CreateSongPlaylistRelationshipAsync(songPlaylist, cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get the playlists by song id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns the playlists by the requested song id</response>
        /// <response code="400">If the given id is equal or less than 0</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{id}/playlists")]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylistsBySong(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0) return BadRequest();

                var result = await _songService.GetPlaylistsBySongAsync(id, cancellationToken);

                result ??= new List<Playlist>();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete the relationship between song and playlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">If the relationship is deleted</response>
        /// <response code="400">If the given ids are equal or less than 0</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{id}/playlists/{playlistId}")]
        public async Task<IActionResult> DeleteSongPlaylistRelationship(int id, int playlistId, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0 || playlistId <= 0) return BadRequest();

                var songPlaylist = new SongPlaylist
                {
                    SongId = id,
                    PlaylistId = playlistId
                };

                await _songService.DeleteSongPlaylistRelationshipAsync(songPlaylist, cancellationToken);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
