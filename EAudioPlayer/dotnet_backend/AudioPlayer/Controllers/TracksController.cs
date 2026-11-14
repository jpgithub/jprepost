using AudioPlayer.Models;
using AudioPlayer.Models.Dtos;
using AudioPlayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace AudioPlayer.Controllers;

/// <summary>Track metadata and audio streaming.</summary>
[ApiController]
[Route("api/tracks")]
public class TracksController : ControllerBase
{
    private static readonly FileExtensionContentTypeProvider ContentTypes = new();

    private readonly IMusicLibrary _library;

    public TracksController(IMusicLibrary library)
    {
        _library = library;
    }

    internal static TrackDto ToDto(Track t, IUrlHelper url) =>
        new(t.Id, t.Title, t.AlbumId, t.AlbumTitle, t.Number, t.Format, t.SizeBytes,
            url.RouteUrl("StreamTrack", new { trackId = t.Id }) ?? $"/api/tracks/{t.Id}/stream");

    /// <summary>Gets metadata for one track.</summary>
    /// <param name="trackId">Track identifier.</param>
    [HttpGet("{trackId}", Name = "GetTrack")]
    [Produces("application/json")]
    [ProducesResponseType<TrackDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public ActionResult<TrackDto> GetTrack(string trackId)
    {
        var track = _library.FindTrack(trackId);
        return track is null ? NotFound() : Ok(ToDto(track, Url));
    }

    /// <summary>Streams the audio file. Supports HTTP range requests for seeking.</summary>
    /// <param name="trackId">Track identifier.</param>
    [HttpGet("{trackId}/stream", Name = "StreamTrack")]
    [Produces("audio/mpeg", "audio/wav", "audio/ogg", "audio/mp4", "audio/flac", "audio/aac")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status206PartialContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status416RangeNotSatisfiable)]
    public IActionResult Stream(string trackId)
    {
        var track = _library.FindTrack(trackId);
        if (track is null || !System.IO.File.Exists(track.FilePath))
        {
            return NotFound();
        }

        if (!ContentTypes.TryGetContentType(track.FilePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        return PhysicalFile(track.FilePath, contentType, enableRangeProcessing: true);
    }
}
