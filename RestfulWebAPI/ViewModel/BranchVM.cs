using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestfulWebAPI.ViewModel
{
   public class BranchVM
   { 
      public int Id { get; set; }
      public string Name { get; set; }
      public string Address { get; set; }
      public string? City { get; set; } 
      public int InstituteID { get; set; }
      public string? InsttitueName { get; set; }
   }
}
