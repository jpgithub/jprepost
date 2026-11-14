using AudioPlayer.Services;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IMusicLibrary, FileSystemMusicLibrary>();

var app = builder.Build();

SampleAudioSeeder.Seed(app.Environment.WebRootPath);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapOpenApi();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
