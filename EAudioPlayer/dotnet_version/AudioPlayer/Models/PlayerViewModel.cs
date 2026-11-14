namespace AudioPlayer.Models;

public class PlayerViewModel
{
    public required IReadOnlyList<Album> Albums { get; init; }
}
