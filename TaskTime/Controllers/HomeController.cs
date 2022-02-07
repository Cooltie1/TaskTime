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
        private TaskContext _taskContext { get; set; }
        public HomeController(ILogger<HomeController> logger, TaskContext taskContext)
        {
            _logger = logger;
            _taskContext = taskContext;

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
        public IActionResult Add_Task()
        {
            ViewBag.categories = _taskContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add_Task(ApplicationResponse appResponse)
        {
            if (ModelState.IsValid)
            {
                _taskContext.Update(appResponse);
                _taskContext.SaveChanges();
                return View("Quadrants");
            }
            ViewBag.categories = _taskContext.Categories.ToList();
            return View("Add_Task");
        }


        public IActionResult Quadrants()
        {
            var tasks = _taskContext.Responses
                .Include(x => x.Category)
                .OrderBy(x => x.DueDate)
                .ToList();
            return View(tasks);
        }

        [HttpPost]
        public IActionResult Edit (ApplicationResponse task)
        {
            _taskContext.Update(task);
            _taskContext.SaveChanges();

            return RedirectToAction("Quadrants");
        }

        [HttpGet]
        public IActionResult Edit(int taskid)
        {
            ViewBag.categories = _taskContext.Categories.ToList();
            var task = _taskContext.Responses.Single(x => x.AppResponseId == taskid);
            return View("Add_Task", task);
        }

        public IActionResult Delete(int taskid)
        {
            _taskContext.Responses.Remove(_taskContext.Responses.Single(x => x.AppResponseId == taskid));
            _taskContext.SaveChanges();
            return RedirectToAction("Quadrants");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
