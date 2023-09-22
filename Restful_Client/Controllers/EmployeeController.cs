using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restful_Client.Controllers
{
   public class EmployeeController : Controller
   {
      public ActionResult Index()
      {
         return View();
      }
   }
}