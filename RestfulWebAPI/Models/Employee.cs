using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestfulWebAPI.Models
{
   public class Employee
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public string ContactNumber { get; set; }
      public string Email { get; set; } 
      [ForeignKey("Branch")]
      public int BranchID { get; set; } 
      public Branch Branch { get; set; }
   }
}
