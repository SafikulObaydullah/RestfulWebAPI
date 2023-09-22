using System.ComponentModel.DataAnnotations;

namespace RestfulWebAPI.Models
{
   public class Institute
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public string Address { get; set; }
      public string Description { get; set; }
   }
}
