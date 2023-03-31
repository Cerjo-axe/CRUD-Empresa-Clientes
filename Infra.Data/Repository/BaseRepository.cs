using Domain.Interfaces.Repository;
using Domain.Models;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Base
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task Add(TEntity obj)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }

    public virtual async Task Delete(TEntity obj)
    {
        try
        {
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();

        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }

    public virtual async Task<TEntity> GetbyId(string id)
    {
        try
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }

    public virtual async Task Update(TEntity obj)
    {
        try
        {
            _context.Entry(obj).State=EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }
}
