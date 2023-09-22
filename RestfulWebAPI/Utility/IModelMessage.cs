namespace RestfulWebAPI.Utility
{
   public interface IModelMessage
   {
      public string Message { get; set; }
      public object EntityModel { get; set; }
   }
}
