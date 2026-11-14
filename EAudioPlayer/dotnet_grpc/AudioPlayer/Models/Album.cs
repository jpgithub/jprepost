namespace AudioPlayer.Models;

public class Album
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required IReadOnlyList<Track> Tracks { get; init; }
}
