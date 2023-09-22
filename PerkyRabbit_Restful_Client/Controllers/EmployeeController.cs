using Microsoft.AspNetCore.Mvc;
using PerkyRabbit_Restful_Client.Models;
using System.Diagnostics;

namespace PerkyRabbit_Restful_Client.Controllers
{
   public class EmployeeController : Controller
   {
      private readonly ILogger<HomeController> _logger;

      public EmployeeController(ILogger<HomeController> logger)
      {
         _logger = logger;
      } 
      public IActionResult Index()
      {
         return View();
      }
   }
}