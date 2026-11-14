using AudioPlayer.Models;

namespace AudioPlayer.Services;

public interface IMusicLibrary
{
    IReadOnlyList<Album> GetAlbums();
    Album? FindAlbum(string albumId);
    Track? FindTrack(string trackId);
}
