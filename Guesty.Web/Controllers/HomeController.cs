using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Guesty.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Guesty.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }       
        public IActionResult Index()
        {
            return View();
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
        [Authorize(Roles = "Team")]
        public IActionResult Team()
        {
            return View();
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Manager()
        {
            return View();
        }
        [Authorize(Roles = "Partner")]
        public IActionResult Partner()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        public IActionResult Owner()
        {
            return View();
        }
        [Authorize(Roles = "Guest")]
        public IActionResult Guest()
        {
            return View();
        }
    }
}
