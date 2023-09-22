using RestfulWebAPI.Utility;

namespace RestfulWebAPI.Helper
{
   public class ModelsMessage : IModelMessage
   {
      public bool IsSuccess { get; set; }
      public string Message { get; set; } = "";
      public object EntityModel { get; set ; }
   }
}
