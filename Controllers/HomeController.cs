using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechJobsAuthentication.Models;
using TechJobsAuthentication.Data;
using TechJobsAuthentication.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TechJobsAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.ToList();

            return View(jobs);
        }
        [Authorize]
        [HttpGet("/Add")]
        public IActionResult Add()
        {
            AddJobViewModel addJobViewModel = new AddJobViewModel();

            return View(addJobViewModel);
        }

        [AllowAnonymous]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel)
        {
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = addJobViewModel.Employer,
                    Skill = addJobViewModel.Skill
                };

                context.Jobs.Add(newJob);
                context.SaveChanges();

                return Redirect("Index");
            }

            return View("Add", addJobViewModel);
        }

    }
}
