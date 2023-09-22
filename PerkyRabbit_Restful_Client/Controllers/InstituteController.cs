using Microsoft.AspNetCore.Mvc;
using PerkyRabbit_Restful_Client.Models;
using System.Diagnostics;

namespace PerkyRabbit_Restful_Client.Controllers
{
   public class InstituteController : Controller
   {
      private readonly ILogger<HomeController> _logger;

      public InstituteController(ILogger<HomeController> logger)
      {
         _logger = logger;
      }

      public IActionResult Index()
      {
         return View();
      }
   }
}