using AudioPlayer.Models;

namespace AudioPlayer.Services;

public interface IMusicLibrary
{
    IReadOnlyList<Album> GetAlbums();
}
