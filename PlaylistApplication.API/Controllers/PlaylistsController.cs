using Microsoft.AspNetCore.Mvc;
using PlaylistApplication.API.Entities;
using PlaylistApplication.API.Services.Interfaces;
using PlaylistApplication.API.Validators;
using System.Net;

namespace PlaylistApplication.API.Controllers
{
    [Route("api/playlists")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPlaylistService _playlistService;
        private readonly PlaylistValidator _playlistValidator;

        public PlaylistsController(ILogger<PlaylistsController> logger, IPlaylistService playlistService, PlaylistValidator playlistValidator)
        {
            _logger = logger;
            _playlistService = playlistService;
            _playlistValidator = playlistValidator;
        }

        /// <summary>
        /// Get a playlist by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns the playlist by the requested id</response>
        /// <response code="400">If the given id is equal or less than 0</response>
        /// <response code="404">If no playlist is found for the requested id</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetOne(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0) return BadRequest();

                var result = await _playlistService.GetOneAsync(id, cancellationToken);

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
        /// Get all the playlists
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns all the playlists</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _playlistService.GetAllAsync(cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create a playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns the id of the playlist created</response>
        /// <response code="400">If the playlist values are invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Playlist playlist, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = _playlistValidator.Validate(playlist);
                if (validationResult.IsValid)
                {
                    var result = await _playlistService.CreateAsync(playlist, cancellationToken);
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
        /// Update a playlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playlist"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">If the playlist is updated</response>
        /// <response code="400">If the playlist values are invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Playlist playlist, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0 || id != playlist.Id) return BadRequest();

                var validationResult = _playlistValidator.Validate(playlist);
                if (validationResult.IsValid)
                {
                    var result = await _playlistService.UpdateAsync(playlist, cancellationToken);
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
        /// Delete a playlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">If the playlist is deleted</response>
        /// <response code="400">If the given id is equal or less than 0</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0) return BadRequest();

                await _playlistService.DeleteAsync(id, cancellationToken);

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
        [HttpPost("{id}/songs")]
        public async Task<IActionResult> CreateSongPlaylistRelationship(int id, [FromBody] SongPlaylist songPlaylist, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0 || id != songPlaylist.PlaylistId) return BadRequest();

                var result = await _playlistService.CreateSongPlaylistRelationshipAsync(songPlaylist, cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get the songs by playlist id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns the songs by the requested playlist id</response>
        /// <response code="400">If the given id is equal or less than 0</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{id}/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByPlaylist(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0) return BadRequest();

                var result = await _playlistService.GetSongsByPlaylistAsync(id, cancellationToken);

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
        /// <param name="songId"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">If the relationship is deleted</response>
        /// <response code="400">If the given ids are equal or less than 0</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{id}/songs/{songId}")]
        public async Task<IActionResult> DeleteSongPlaylistRelationship(int id, int songId, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= 0 || songId <= 0) return BadRequest();

                var songPlaylist = new SongPlaylist
                {
                    SongId = songId,
                    PlaylistId = id
                };

                await _playlistService.DeleteSongPlaylistRelationshipAsync(songPlaylist, cancellationToken);

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
