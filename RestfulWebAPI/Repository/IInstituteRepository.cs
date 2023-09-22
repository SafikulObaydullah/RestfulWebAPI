using RestfulWebAPI.Models;

namespace RestfulWebAPI.Repository
{
   public interface IInstituteRepository : IBaseRepository<Institute>
   {
      List<InstituteVM> GetInstitute();
      void Update(Institute entity);
   }
   public class InstituteRepository : BaseRepository<Institute>, IInstituteRepository
   {
      private ApplicationDbContext _context;
      public InstituteRepository(ApplicationDbContext context) : base(context)
      {
         _context = context;
      }
      public List<InstituteVM> GetInstitute()
      {
         var data = _context.Institute.ToList();
         List<InstituteVM> result = _context.Institute.Select(s =>
                       new InstituteVM
                       {
                          Id = s.Id,
                          Name = s.Name, 
                          Address = s.Address,
                          Description = s.Description,
                          Type = s.Type,
                       }).ToList();
         return result;
      }
      public void Update(Institute entity)
      {
         _context.Institute.Update(entity);
      }
   }
}
