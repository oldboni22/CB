using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.PagedList;
using UserService.Domain.Context;
using UserService.Domain.Data.Models;
using UserService.Domain.Interfaces.Repository;

namespace UserService.Infrastructure.Repositories;

public abstract class GenericRepository<T>(UserServiceDbContext context) : IGenericRepository<T> where T : EntityBase
{
    protected readonly UserServiceDbContext Context = context;
    
    protected async Task<PagedList<T>> GetByConditionAsync(
        Expression<Func<T, bool>> expression, PaginationParameters paginationParameters, bool trackChanges = true)
    {
        var query = Context
            .Set<T>()
            .Where(expression);
        
        query = trackChanges ? query : query.AsNoTracking();
        
        var totalCount = await query.CountAsync();
        var pagesCount = (int)Math.Ceiling(totalCount / (double)paginationParameters.PageSize);

        var list = await query
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .ToListAsync();

        return list.ToPagedList(paginationParameters.PageNumber, paginationParameters.PageSize, totalCount, pagesCount);
    }

    public async Task<T?> GetByIdAsync(Guid id,  bool trackChanges = true)
    { 
        var query = Context
            .Set<T>()
            .Where(entity => entity.Id == id);
        
        query = trackChanges ? query : Context.Set<T>().AsNoTracking();
        
        return await query.SingleOrDefaultAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id, false);

        if (entity is null)
        {
            return false;
        }
        
        Context.Set<T>().Remove(entity);
        
        return true;
    }

    public async Task CreateAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        var targetEntity = await GetByIdAsync(entity.Id);

        if (targetEntity is null)
        {
            return null;
        }
        
        Context.Entry(targetEntity).CurrentValues.SetValues(entity);

        return targetEntity;
    }
}
