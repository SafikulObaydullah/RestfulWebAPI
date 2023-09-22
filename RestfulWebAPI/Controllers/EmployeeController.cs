using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestfulWebAPI.Helper;
using RestfulWebAPI.Models;
using RestfulWebAPI.Repository;
using RestfulWebAPI.ViewModel;
using System.Dynamic;
using System.Text;

namespace RestfulWebAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [EnableCors("Powersoft")]
   public class EmployeeController : ControllerBase
   {
      private IUnitofWork unitofWork;
      ModelsMessage modelsMessage; 
      public EmployeeController(IUnitofWork unitofWork)
      {
         this.unitofWork = unitofWork;
         modelsMessage = new ModelsMessage(); 
      }

      [HttpGet("GetEmployee")]
      public IEnumerable<EmployeeVM> GetAll()
      {
         List<EmployeeVM> all = new List<EmployeeVM>();
         try
         {
            all = this.unitofWork.EmployeeRepository.GetEmployee().ToList();
         }
         catch (Exception ex)
         {
            all = new List<EmployeeVM>();
         } 
         return all;
      }
      [HttpGet("GetByID")]
      public IEnumerable<Employee> GetByID(int Id)
      {
         List<Employee> all = new List<Employee>();
         try
         {
            all = this.unitofWork.EmployeeRepository.GetByID(t => t.Id.Equals(Id)).ToList();
         }
         catch (Exception ex)
         {
            all = new List<Employee>();
         }
         return all;
      }
      [HttpPost("SaveEmployee")]
      public IActionResult Save(EmployeeVM model)
      {
         Employee Employee = new Employee()
         {
            Name = model.Name,
            Email = model.Email,
            ContactNumber = model.ContactNumber,
            BranchID = model.BranchID, 
         }; 
         try
         {
            this.unitofWork.EmployeeRepository.Add(Employee);
            var m = this.unitofWork.Save();
            if (m.IsSuccess)
            {
               return Ok(new { Data = model, result = m });
            }
            else
            {
               return Problem(m.Message);
            }
         }
         catch (Exception ex)
         {
            return Problem(ex.Message);
         }
      }
      [HttpPut("UpdateEmployee")]
      public IActionResult Update(EmployeeVM model)
      { 
         var data = unitofWork.EmployeeRepository.GetByID(t => t.Id.Equals(model.Id)).FirstOrDefault();
         if(data == null)
         {
            throw new Exception("Data Not Found");
         };
         data.Name = model.Name;
         data.Email = model.Email;
         data.ContactNumber = model.ContactNumber;
         data.BranchID = model.BranchID;

         try
         {
            this.unitofWork.EmployeeRepository.Update(data);
            var m = this.unitofWork.Save();
            if (m.IsSuccess)
            {
               return Ok(new { Data = data, result = m });
            }
            else
            {
               return Problem(m.Message);
            }
         }
         catch (Exception ex)
         {
            return Problem(ex.Message);
         }
      }
      [HttpDelete]
      [Route("Delete")]
      public IActionResult Delete(int id)
      {
         try
         {
            this.unitofWork.EmployeeRepository.DeletebyID(t => t.Id.Equals(id));
            var m = this.unitofWork.Save();
            if (m.IsSuccess)
            {
               return Ok(new { result = m });
            }
            else
            {
               return Problem(m.Message);
            }
         }
         catch (Exception ex)
         {
            return Problem(ex.Message);
         }
      }
      [HttpGet("GetInitialData")]
      public JsonResult GetInitialData()
      {
         dynamic result = new ExpandoObject();
         try
         {
            result.branches = this.unitofWork.InstituteBranchRepository.GetAll(null, null).ToList();
         }
         catch (Exception ex)
         {
            ModelState.AddModelError("Failed", ex.Message);
         }
         return Json(result);
      }
      [NonAction]
      public virtual JsonResult Json(object? data)
      {
         return new JsonResult(data);
      }
   }
}