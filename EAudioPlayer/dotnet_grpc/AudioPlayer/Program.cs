using AudioPlayer.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<LibraryOptions>(builder.Configuration.GetSection(LibraryOptions.SectionName));
builder.Services.AddSingleton<IMusicLibrary, FileSystemMusicLibrary>();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

// Browser clients call over gRPC-Web from another origin and need the gRPC headers exposed.
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p
    .WithOrigins(allowedOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")));

var app = builder.Build();

var libraryOptions = app.Services.GetRequiredService<IOptions<LibraryOptions>>().Value;
SampleAudioSeeder.Seed(FileSystemMusicLibrary.ResolveRoot(app.Environment.ContentRootPath, libraryOptions.AudioPath));

app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors();

app.MapGrpcService<AudioLibraryService>().RequireCors();
app.MapGrpcReflectionService().RequireCors();
app.MapGet("/", () => "AudioPlayer gRPC service. Use a gRPC or gRPC-Web client.");

app.Run();
