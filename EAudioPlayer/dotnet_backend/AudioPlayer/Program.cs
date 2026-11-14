using AudioPlayer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<LibraryOptions>(builder.Configuration.GetSection(LibraryOptions.SectionName));
builder.Services.AddSingleton<IMusicLibrary, FileSystemMusicLibrary>();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "Audio Player API";
        document.Info.Version = "v1";
        document.Info.Description = "Albums, tracks and audio streaming for the audio player.";
        return Task.CompletedTask;
    });
});

// Browser front ends (e.g. the Angular app) call this API from another origin.
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p
    .WithOrigins(allowedOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("Accept-Ranges", "Content-Range", "Content-Length")));

var app = builder.Build();

var libraryOptions = app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<LibraryOptions>>().Value;
SampleAudioSeeder.Seed(FileSystemMusicLibrary.ResolveRoot(app.Environment.ContentRootPath, libraryOptions.AudioPath));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStatusCodePages();
app.UseCors();

app.MapOpenApi();
app.UseSwaggerUi(options =>
{
    options.DocumentPath = "/openapi/v1.json";
    options.Path = "/swagger";
});

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.MapControllers();

app.Run();
