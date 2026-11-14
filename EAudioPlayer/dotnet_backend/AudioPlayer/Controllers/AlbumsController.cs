using AudioPlayer.Models.Dtos;
using AudioPlayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AudioPlayer.Controllers;

/// <summary>Browse albums and the tracks they contain.</summary>
[ApiController]
[Route("api/albums")]
[Produces("application/json")]
public class AlbumsController : ControllerBase
{
    private readonly IMusicLibrary _library;

    public AlbumsController(IMusicLibrary library)
    {
        _library = library;
    }

    /// <summary>Lists all albums.</summary>
    [HttpGet(Name = "GetAlbums")]
    [ProducesResponseType<IReadOnlyList<AlbumSummaryDto>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<AlbumSummaryDto>> GetAlbums() =>
        Ok(_library.GetAlbums().Select(a => new AlbumSummaryDto(a.Id, a.Title, a.Tracks.Count)).ToList());

    /// <summary>Gets one album with its tracks.</summary>
    /// <param name="albumId">Album identifier.</param>
    [HttpGet("{albumId}", Name = "GetAlbum")]
    [ProducesResponseType<AlbumDetailDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public ActionResult<AlbumDetailDto> GetAlbum(string albumId)
    {
        var album = _library.FindAlbum(albumId);
        return album is null
            ? NotFound()
            : Ok(new AlbumDetailDto(album.Id, album.Title, album.Tracks.Select(t => TracksController.ToDto(t, Url)).ToList()));
    }

    /// <summary>Lists the tracks of one album in play order.</summary>
    /// <param name="albumId">Album identifier.</param>
    [HttpGet("{albumId}/tracks", Name = "GetAlbumTracks")]
    [ProducesResponseType<IReadOnlyList<TrackDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<TrackDto>> GetAlbumTracks(string albumId)
    {
        var album = _library.FindAlbum(albumId);
        return album is null
            ? NotFound()
            : Ok(album.Tracks.Select(t => TracksController.ToDto(t, Url)).ToList());
    }
}
