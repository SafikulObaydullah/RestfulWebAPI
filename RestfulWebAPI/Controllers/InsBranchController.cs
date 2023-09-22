using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestfulWebAPI.Helper;
using RestfulWebAPI.Models;
using RestfulWebAPI.Repository;
using RestfulWebAPI.ViewModel;
using System.Text;

namespace RestfulWebAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [EnableCors("Powersoft")]
   public class InsBranchController : ControllerBase
   {
      private IUnitofWork unitofWork;
      ModelsMessage modelsMessage; 
      public InsBranchController(IUnitofWork unitofWork)
      {
         this.unitofWork = unitofWork;
         modelsMessage = new ModelsMessage(); 
      }

      [HttpGet("GetBranch")]
      public IEnumerable<BranchVM> GetAll()
      {
         List<BranchVM> all = new List<BranchVM>();
         try
         {
            all = this.unitofWork.InstituteBranchRepository.GetInstituteBranch().ToList();
         }
         catch (Exception ex)
         {
            all = new List<BranchVM>();
         } 
         return all;
      }
      [HttpGet("GetByID")]
      public IEnumerable<Branch> GetByID(int Id)
      {
         List<Branch> all = new List<Branch>();
         try
         {
            all = this.unitofWork.InstituteBranchRepository.GetByID(t => t.Id.Equals(Id)).ToList();
         }
         catch (Exception ex)
         {
            all = new List<Branch>();
         }
         return all;
      }
      [HttpPost("SaveBranch")]
      public IActionResult Save(BranchVM model)
      {
         Branch branch = new Branch()
         {
            Name = model.Name,
            Address = model.Address,
            City = model.City,
            InstituteID = model.InstituteID,
         }; 
         try
         {
            this.unitofWork.InstituteBranchRepository.Add(branch);
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
      [HttpPut("UpdateBranch")]
      public IActionResult Update(Branch Branch)
      {
         try
         {
            this.unitofWork.InstituteBranchRepository.Update(Branch);
            var m = this.unitofWork.Save();
            if (m.IsSuccess)
            {
               return Ok(new { Data = Branch, result = m });
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
            this.unitofWork.InstituteBranchRepository.DeletebyID(t => t.Id.Equals(id));
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