using AlternativeMedicine.App.Domain.Constants;
using AlternativeMedicine.App.Domain.Entities;
using System.Linq.Expressions;

namespace AlternativeMedicine.App.DataAccess;

public interface IBaseRepository<T> where T : class
{
    T GetById(int id);
    Task<T> GetByIdAsync(int id);
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> Paginate(int pageNumber = 1, int pageSize = 10, string[] includes = null);
    Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
    T Add(T entity);
    Task<T> AddAsync(T entity);
    T Update(T entity);
    void Delete(T entity);
    int Count();
    Task<int> CountAsync();
    int Count(Expression<Func<T, bool>> criteria);
    Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int pageNumber = 1, int pageSize = 10, string[] includes = null);
}   