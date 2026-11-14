using AudioPlayer.Grpc;
using Grpc.Core;
using Microsoft.AspNetCore.StaticFiles;

namespace AudioPlayer.Services;
// service contract grpc.proto parent class AudioLibrary.AudioLibraryBase auto-generated
public class AudioLibraryService : AudioLibrary.AudioLibraryBase
{
    private const int ChunkSize = 64 * 1024;
    private static readonly FileExtensionContentTypeProvider ContentTypes = new();

    private readonly IMusicLibrary _library;

    public AudioLibraryService(IMusicLibrary library)
    {
        _library = library;
    }

    public override Task<ListAlbumsResponse> ListAlbums(ListAlbumsRequest request, ServerCallContext context)
    {
        var response = new ListAlbumsResponse();
        response.Albums.AddRange(_library.GetAlbums()
            .Select(a => new AlbumSummary { Id = a.Id, Title = a.Title, TrackCount = a.Tracks.Count }));
        return Task.FromResult(response);
    }

    public override Task<AlbumDetail> GetAlbum(GetAlbumRequest request, ServerCallContext context)
    {
        var album = _library.FindAlbum(request.AlbumId)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Album '{request.AlbumId}' not found."));

        var detail = new AlbumDetail { Id = album.Id, Title = album.Title };
        detail.Tracks.AddRange(album.Tracks.Select(ToInfo));
        return Task.FromResult(detail);
    }

    public override Task<TrackInfo> GetTrack(GetTrackRequest request, ServerCallContext context)
    {
        var track = _library.FindTrack(request.TrackId)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Track '{request.TrackId}' not found."));
        return Task.FromResult(ToInfo(track));
    }

    public override async Task StreamTrack(StreamTrackRequest request, IServerStreamWriter<AudioChunk> responseStream, ServerCallContext context)
    {
        var track = _library.FindTrack(request.TrackId);
        if (track is null || !File.Exists(track.FilePath))
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Track '{request.TrackId}' not found."));
        }

        await using var file = new FileStream(track.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, ChunkSize, useAsync: true);
        if (request.Offset < 0 || request.Offset > file.Length)
        {
            throw new RpcException(new Status(StatusCode.OutOfRange, "Offset is outside the file."));
        }

        file.Seek(request.Offset, SeekOrigin.Begin);
        var buffer = new byte[ChunkSize];
        var position = request.Offset;
        int read;
        while ((read = await file.ReadAsync(buffer, context.CancellationToken)) > 0)
        {
            await responseStream.WriteAsync(new AudioChunk
            {
                Data = Google.Protobuf.ByteString.CopyFrom(buffer, 0, read),
                Offset = position,
                TotalSize = file.Length
            }, context.CancellationToken);
            position += read;
        }
    }

    private static TrackInfo ToInfo(Models.Track t)
    {
        if (!ContentTypes.TryGetContentType(t.FilePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        return new TrackInfo
        {
            Id = t.Id,
            Title = t.Title,
            AlbumId = t.AlbumId,
            AlbumTitle = t.AlbumTitle,
            Number = t.Number,
            Format = t.Format,
            SizeBytes = t.SizeBytes,
            ContentType = contentType
        };
    }
}
