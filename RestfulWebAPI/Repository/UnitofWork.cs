using RestfulWebAPI.Helper;
using RestfulWebAPI.Models;

namespace RestfulWebAPI.Repository
{
   public class UnitofWork : IUnitofWork
   {
      #region field
      private ApplicationDbContext _context;
      private IInstituteRepository _instituteRepository;
      private InstituteBranchRepository _branchRepository;
      private IEmployeeRepository _EmployeeRepository;

      InstituteBranchRepository _insBranch {  get; set; }   
      #endregion
      public UnitofWork(ApplicationDbContext context)
      {
         _context = context; 
      }
      public ApplicationDbContext DbContext
      {
         get { return _context; }

      } 
      public IInstituteRepository InstituteRepository
      {
         get
         {
            if (_instituteRepository == null)
            {
               _instituteRepository = new InstituteRepository(_context);
            }
            return _instituteRepository;
         }
      }
      public IInstituteBranchRepository InstituteBranchRepository
      {
         get
         {
            if (_insBranch == null)
            {
               _insBranch = new InstituteBranchRepository(_context);
            }
            return _insBranch;
         }
      }
      public IEmployeeRepository EmployeeRepository
      {
         get
         {
            if (_EmployeeRepository == null)
            {
               _EmployeeRepository = new EmployeeRepository(_context);
            }
            return _EmployeeRepository;
         }
      }
      public ModelsMessage Save()
      {
         ModelsMessage modelMessage = new ModelsMessage();
         string msg = "";
         try
         {
            if (_context.SaveChanges() > 0)
            {
               modelMessage.Message = $"Action committed Successfully ";
               modelMessage.IsSuccess = true;
            }
            else
            {
               modelMessage.Message = "Action Failed";
               modelMessage.IsSuccess = false;
            }
         } 
         catch (Exception ex)
         {
            if (ex.InnerException != null)
            { 
               modelMessage.Message = ex.InnerException.Message;
               modelMessage.IsSuccess = false;
            }
            else
            {
               modelMessage.Message = ex.Message;
               modelMessage.IsSuccess = false;
            }
         }
         return modelMessage;
      }
   }
   public interface IUnitofWork
   {
      IInstituteRepository InstituteRepository { get; } 
      IInstituteBranchRepository InstituteBranchRepository { get; }
      IEmployeeRepository EmployeeRepository { get; }
      ModelsMessage Save();
   }
}
