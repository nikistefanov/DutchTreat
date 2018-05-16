using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IDutchRepository repository;

        public AppController(IMailService mailService, IDutchRepository repository)
        {
            this.mailService = mailService;
            this.repository = repository;
        }

        // Action Index is the same as the View/App/Index.cshtml. When App controller is called in Startup.cs it will trigger the action Index() which will go look for that Index.cshtml file.
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("/contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send the email
                this.mailService.SendMessage("n.stefanov@outlook.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail sent!";
                ModelState.Clear();
            }
            else
            {
                // Show the errors
            }

            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var results = this.repository.GetAllProducts();

            return View(results);
        }
    }
}
