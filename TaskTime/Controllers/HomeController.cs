using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskTime.Models;

namespace TaskTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private TaskContext TaskContext { get; set; }
        public HomeController(TaskContext x)
        {
            TaskContext = x;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TaskList()
        {
            var Tasks = TaskContext.Responses
                .Include(x => x.Category)
                .Where(x=> x.Completed == false)
                .ToList();

            return View(Tasks);
        }
        [HttpGet]
        public IActionResult Add_Task()
        {
            ViewBag.Categories = TaskContext.Categories.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Add_Task(ApplicationResponse ar)
        {
            TaskContext.add(ar);
            TaskContext.savechanges();
            return RedirectToAction("TaskList");
        }

        public IActionResult Quadrants()
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
