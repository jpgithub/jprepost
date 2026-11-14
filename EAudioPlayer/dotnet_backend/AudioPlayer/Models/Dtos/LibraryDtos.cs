namespace AudioPlayer.Models.Dtos;

/// <summary>Album summary.</summary>
/// <param name="Id">Stable identifier used in API routes.</param>
/// <param name="Title">Album name.</param>
/// <param name="TrackCount">Number of tracks in the album.</param>
public record AlbumSummaryDto(string Id, string Title, int TrackCount);

/// <summary>Album including its ordered tracks.</summary>
public record AlbumDetailDto(string Id, string Title, IReadOnlyList<TrackDto> Tracks);

/// <summary>Track metadata.</summary>
/// <param name="Id">Stable identifier used in API routes.</param>
/// <param name="Title">Song title.</param>
/// <param name="AlbumId">Identifier of the owning album.</param>
/// <param name="AlbumTitle">Name of the owning album.</param>
/// <param name="Number">1-based position within the album.</param>
/// <param name="Format">Audio format, e.g. MP3 or WAV.</param>
/// <param name="SizeBytes">File size in bytes.</param>
/// <param name="StreamUrl">Relative URL that streams the audio (supports HTTP range requests).</param>
public record TrackDto(
    string Id,
    string Title,
    string AlbumId,
    string AlbumTitle,
    int Number,
    string Format,
    long SizeBytes,
    string StreamUrl);
