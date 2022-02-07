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

        [HttpPost]
        public IActionResult Edit (ApplicationResponse task)
        {
            _taskContext.Update(task);
            _taskContext.SaveChanges();

            return RedirectToAction("TaskList");
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
            return RedirectToAction("TaskList");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
