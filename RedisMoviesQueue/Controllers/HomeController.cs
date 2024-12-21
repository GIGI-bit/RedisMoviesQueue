using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RedisMoviesQueue.Models;
using RedisMoviesQueue.Services;

namespace RedisMoviesQueue.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IQueueService _queueService;

        public HomeController(ILogger<HomeController> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string movieName)
        {
            await _queueService.SendMessageAsync(movieName);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
