using System.Linq.Expressions;
using Shared.PagedList;
using UserService.Domain.Data.Models;

namespace UserService.Domain.Interfaces.Repository;

public interface IGenericRepository<T> where T : EntityBase
{
    Task<T?> GetByIdAsync(Guid id, bool trackChanges = true);
    
    Task<bool> DeleteAsync(Guid id);
    
    Task CreateAsync(T entity);
    
    Task<T?> UpdateAsync(T entity);
}