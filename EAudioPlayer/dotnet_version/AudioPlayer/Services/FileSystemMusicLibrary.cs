using AudioPlayer.Models;

namespace AudioPlayer.Services;

/// <summary>
/// Builds the library from wwwroot/audio: every sub-folder is an album and every
/// supported audio file inside it is a track.
/// </summary>
public class FileSystemMusicLibrary : IMusicLibrary
{
    private static readonly HashSet<string> SupportedExtensions =
        new(StringComparer.OrdinalIgnoreCase) { ".mp3", ".wav", ".ogg", ".m4a", ".flac", ".aac" };

    private readonly string _root;

    public FileSystemMusicLibrary(IWebHostEnvironment env)
    {
        _root = Path.Combine(env.WebRootPath, SampleAudioSeeder.AudioFolder);
    }

    public IReadOnlyList<Album> GetAlbums()
    {
        if (!Directory.Exists(_root))
        {
            return Array.Empty<Album>();
        }

        var albums = new List<Album>();
        foreach (var dir in Directory.EnumerateDirectories(_root).OrderBy(d => d, StringComparer.OrdinalIgnoreCase))
        {
            var albumName = Path.GetFileName(dir);
            var albumId = "album-" + albums.Count;

            var files = Directory.EnumerateFiles(dir)
                .Where(f => SupportedExtensions.Contains(Path.GetExtension(f)))
                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (files.Count == 0)
            {
                continue;
            }

            var tracks = files.Select((file, i) => new Track
            {
                Id = $"{albumId}-track-{i}",
                Title = CleanTitle(Path.GetFileNameWithoutExtension(file)),
                AlbumId = albumId,
                AlbumTitle = albumName,
                Number = i + 1,
                Url = $"/{SampleAudioSeeder.AudioFolder}/{Uri.EscapeDataString(albumName)}/{Uri.EscapeDataString(Path.GetFileName(file))}",
                Format = Path.GetExtension(file).TrimStart('.').ToUpperInvariant(),
                SizeBytes = new FileInfo(file).Length
            }).ToList();

            albums.Add(new Album { Id = albumId, Title = albumName, Tracks = tracks });
        }

        return albums;
    }

    // "01 - My Song" or "01_My_Song" -> "My Song"
    private static string CleanTitle(string name)
    {
        var title = name.TrimStart("0123456789".ToCharArray()).TrimStart(' ', '-', '_', '.');
        return string.IsNullOrWhiteSpace(title) ? name : title.Replace('_', ' ');
    }
}
