namespace AudioPlayer.Models;

public class Track
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string AlbumId { get; init; }
    public required string AlbumTitle { get; init; }
    public required int Number { get; init; }
    public required string FilePath { get; init; }
    public required string Format { get; init; }
    public required long SizeBytes { get; init; }
}
