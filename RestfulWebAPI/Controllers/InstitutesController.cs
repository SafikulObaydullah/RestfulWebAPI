using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestfulWebAPI.Helper;
using RestfulWebAPI.Models;
using RestfulWebAPI.Repository;
using System.Text;

namespace RestfulWebAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [EnableCors("Powersoft")]
   public class InstitutesController : ControllerBase
   {
      private IUnitofWork unitofWork;
      ModelsMessage modelsMessage; 
      public InstitutesController(IUnitofWork unitofWork)
      {
         this.unitofWork = unitofWork;
         modelsMessage = new ModelsMessage(); 
      }

      [HttpGet("GetInstitute")]
      public IEnumerable<Institute> GetAll()
      {
         List<Institute> all = new List<Institute>();
         try
         {
            all = this.unitofWork.InstituteRepository.GetAll(null, null).ToList();
         }
         catch (Exception ex)
         {
            all = new List<Institute>();
         } 
         return all;
      }
      [HttpGet("GetByID")]
      public IEnumerable<Institute> GetByID(int Id)
      {
         List<Institute> all = new List<Institute>();
         try
         {
            all = this.unitofWork.InstituteRepository.GetByID(t => t.Id.Equals(Id)).ToList();
         }
         catch (Exception ex)
         {
            all = new List<Institute>();
         }
         return all;
      }
      [HttpPost("SaveInstitute")]
      public IActionResult Save(Institute institute)
      {
         try
         {
            this.unitofWork.InstituteRepository.Add(institute);
            var m = this.unitofWork.Save();
            if (m.IsSuccess)
            {
               return Ok(new { Data = institute, result = m });
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
      [HttpPut("UpdateInstitute")]
      public IActionResult Update(Institute institute)
      {
         try
         {
            this.unitofWork.InstituteRepository.Update(institute);
            var m = this.unitofWork.Save();
            if (m.IsSuccess)
            {
               return Ok(new { Data = institute, result = m });
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
            this.unitofWork.InstituteRepository.DeletebyID(t => t.Id.Equals(id));
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
   }
}