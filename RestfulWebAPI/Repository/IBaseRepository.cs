using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using RestfulWebAPI.Models;

namespace RestfulWebAPI.Repository
{
   public interface IBaseRepository<T> where T : class
   { 
      IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate, string? includeProperties);
      IEnumerable<T> GetAllbyParent(Expression<Func<T, bool>> predicate);
      IEnumerable<T> GetByID(Expression<Func<T, bool>> predicate); 
      void DeletebyID(Expression<Func<T, bool>> predicate);
      void Add(T entity);
      void Add(List<T> entity); 
   }

   public class BaseRepository<T> : IBaseRepository<T> where T : class
   {

      private readonly ApplicationDbContext _context;
      private DbSet<T> _dbset;
      public BaseRepository(ApplicationDbContext context)
      {
         _context = context;
         _dbset = _context.Set<T>();
      }

      public void Add(T entity)
      {
         _dbset.Add(entity);
      }
      public void Add(List<T> entity)
      {
         _dbset.AddRange(entity);
      } 

      public void DeletebyID(Expression<Func<T, bool>> predicate)
      {
         var entity = _dbset.Where(predicate).FirstOrDefault();
         _dbset.Remove(entity);
      }  
      public IEnumerable<T> GetByID(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return _dbset.Where(predicate).ToList();
         }
         catch (Exception ex)
         {
            return null;
         }
      }
       
      public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate, string? includeProperties)
      {
         IQueryable<T> query = _dbset;
         try
         {
            if (predicate != null)
            {
               query = query.Where(predicate);
            }
            if (includeProperties != null)
            {
               foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
               {
                  query = query.Include(item);
               }
            }
            return query.ToList();
         }
         catch (Exception ex)
         {
            return query.ToList();
         }

      }

      public IEnumerable<T> GetAllbyParent(Expression<Func<T, bool>> predicate)
      {
         IQueryable<T> query = _dbset;
         try
         {
            if (predicate != null)
            {
               query = query.Where(predicate);
            }

            return query.ToList();
         }
         catch (Exception ex)
         {
            return query.ToList();

         }

      }
   }
}
