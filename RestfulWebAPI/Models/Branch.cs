using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestfulWebAPI.Models
{
   public class Branch
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      [ForeignKey("Institute")]
      public int InstituteID { get; set; }
      public Institute Institute { get; set; }
   }
}
