using System.Diagnostics;
using AudioPlayer.Models;
using AudioPlayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AudioPlayer.Controllers;

public class HomeController : Controller
{
    private readonly IMusicLibrary _library;

    public HomeController(IMusicLibrary library)
    {
        _library = library;
    }

    public IActionResult Index()
    {
        return View(new PlayerViewModel { Albums = _library.GetAlbums() });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
