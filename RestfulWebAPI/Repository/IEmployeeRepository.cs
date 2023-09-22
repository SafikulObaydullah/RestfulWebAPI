using RestfulWebAPI.Models;
using RestfulWebAPI.ViewModel;

namespace RestfulWebAPI.Repository
{
   public interface IEmployeeRepository : IBaseRepository<Employee>
   {
      List<EmployeeVM> GetEmployee();
      void Update(Employee entity);
   }
   public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
   {
      private ApplicationDbContext _context;
      public EmployeeRepository(ApplicationDbContext context) : base(context)
      {
         _context = context;
      }
      public List<EmployeeVM> GetEmployee()
      {
         var data = _context.Institute.ToList();
         List<EmployeeVM> result = (from a in _context.Employees 
                                 join b  in _context.Branches on a.BranchID equals b.Id
                                 select new EmployeeVM
                                 {
                                    Name = a.Name,
                                    Email = a.Email,  
                                    ContactNumber = a.ContactNumber,
                                    BranchID = a.BranchID,
                                    BranchName = b.Name

                                 }).ToList();
         return result;
      } 

      public void Update(Employee entity)
      {
         _context.Employees.Update(entity);
      }
   }
}
