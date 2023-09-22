using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestfulWebAPI.ViewModel
{
   public class EmployeeVM
   { 
      public int Id { get; set; }
      public string Name { get; set; }
      public string ContactNumber { get; set; }
      public string Email { get; set; }  
      public int BranchID { get; set; }  
      public string? BranchName { get; set; }
   }
}
