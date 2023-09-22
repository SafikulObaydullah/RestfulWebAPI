using RestfulWebAPI.Models;
using RestfulWebAPI.ViewModel;

namespace RestfulWebAPI.Repository
{
   public interface IInstituteBranchRepository : IBaseRepository<Branch>
   {
      List<BranchVM> GetInstituteBranch();
      void Update(Branch entity);
   }
   public class InstituteBranchRepository : BaseRepository<Branch>, IInstituteBranchRepository
   {
      private ApplicationDbContext _context;
      public InstituteBranchRepository(ApplicationDbContext context) : base(context)
      {
         _context = context;
      }
      public List<BranchVM> GetInstituteBranch()
      {
         var data = _context.Institute.ToList();
         List<BranchVM> result = (from a in _context.Branches 
                                 join b  in _context.Institute on a.InstituteID equals b.Id
                                 select new BranchVM
                                 {
                                    Id = a.Id,
                                    Name = a.Name,
                                    Address = a.Address, 
                                    City = a.City, 
                                    InstituteID = a.InstituteID,
                                    InsttitueName = b.Name

                                 }).ToList();
         return result;
      }
      public void Update(Branch entity)
      {
         _context.Branches.Update(entity);
      }
   }
}
