using AppMVCBasic.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppMVCBasic.UI.Controllers
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

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var errorModel = new ErrorViewModel() { ErrorCode = id };

            switch (id)
            {
                case 403:
                    errorModel.Message = "You do not have permission for it.";
                    errorModel.Title = "Access Denied.";
                    break;
                case 404:
                    errorModel.Message = "The page you are looking for does not exist.<br />If you have doubts contact our support.";
                    errorModel.Title = "Page not found.";
                    break;
                default:
                    errorModel.Message = "There was an error! Try again later or contact our support.";
                    errorModel.Title = "There was an error!";
                    break;
            }

            return View("Error", errorModel);
        }
    }
}