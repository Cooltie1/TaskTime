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
        private TaskContext _taskContext { get; set; }
        public HomeController( TaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        //gets the index page
        public IActionResult Index()
        {
            return View();
        }


        

        //CREATE
        [HttpGet]
        public IActionResult Add_Task()
        {
            ViewBag.Categories = _taskContext.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add_Task(ApplicationResponse ar)
        {
            _taskContext.Add(ar);
            _taskContext.SaveChanges();
            return View("ViewTasks");
        }



        //READ


        [HttpGet]
        public IActionResult ViewTasks()
        {
            ViewBag.Categories = _taskContext.Categories.ToList();

            var application = _taskContext.Responses
                .Include(x => x.Category)
                .Where(x => x.Completed == false)
                .OrderBy(x => x.Task)
                .ToList();

            return View(application);
            //return View();
        }


        //EDIT
        [HttpGet]
        public IActionResult Edit(int taskid)
        {
            ViewBag.categories = _taskContext.Categories.ToList();
            var task = _taskContext.Responses.Single(x => x.AppResponseId == taskid);
            return View("Add_Task", task);
        }


        [HttpPost]
        public IActionResult Edit (ApplicationResponse ar)
        {
            
            _taskContext.Update(ar);
            _taskContext.SaveChanges();

            return RedirectToAction("ViewTasks");
        }

       

        //DELETE
        public IActionResult Delete(int taskid)
        {
            _taskContext.Responses.Remove(_taskContext.Responses.Single(x => x.AppResponseId == taskid));
            _taskContext.SaveChanges();
            return RedirectToAction("ViewTasks");
        }

    }
}
