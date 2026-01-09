using Microsoft.EntityFrameworkCore;
using CommandsService.Data.Repositories.Abstract;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandsService.Data.Repositories.Concrate;


public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected AppDbContext _context { get; private set; }
    protected DbSet<TEntity> _dbSet { get; private set; }

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges)
    {
        return trackChanges ? await _dbSet.ToListAsync() : await _dbSet.AsNoTracking().ToListAsync();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges, params string[] includeStrings)
    {
        var query = _dbSet.AsQueryable();
        foreach (var includeString in includeStrings)
        {
            query = query.Include(includeString);
        }
        return trackChanges ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges)
    {
        return trackChanges ? await _dbSet.Where(predicate).ToListAsync() : await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? await query.Where(predicate).ToListAsync() : await query.AsNoTracking().Where(predicate).ToListAsync();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? await query.Where(predicate).ToListAsync() : await query.AsNoTracking().Where(predicate).ToListAsync();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
    }

    public virtual IEnumerable<TEntity> GetAll(bool trackChanges)
    {
        return trackChanges ? _dbSet.ToList() : _dbSet.AsNoTracking().ToList();
    }
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool trackChanges)
    {
        return trackChanges ? _dbSet.Where(predicate).ToList() : _dbSet.AsNoTracking().Where(predicate).ToList();
    }
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? query.Where(predicate).ToList() : query.AsNoTracking().Where(predicate).ToList();
    }
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? query.Where(predicate).ToList() : query.AsNoTracking().Where(predicate).ToList();
    }
    public virtual IEnumerable<TEntity> GetAll(bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? query.ToList() : query.AsNoTracking().ToList();
    }
    public virtual IEnumerable<TEntity> GetAll(bool trackChanges, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? query.ToList() : query.AsNoTracking().ToList();
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges)
    {
        return trackChanges ? await _dbSet.FirstOrDefaultAsync(predicate) : await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
    }
    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? await query.FirstOrDefaultAsync(predicate) : await query.AsNoTracking().FirstOrDefaultAsync(predicate);
    }
    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? await query.FirstOrDefaultAsync(predicate) : await query.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackChanges)
    {
        return trackChanges ? _dbSet.FirstOrDefault(predicate) : _dbSet.AsNoTracking().FirstOrDefault(predicate);
    }
    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? query.FirstOrDefault(predicate) : query.AsNoTracking().FirstOrDefault(predicate);
    }
    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return trackChanges ? query.FirstOrDefault(predicate) : query.AsNoTracking().FirstOrDefault(predicate);
    }

    public virtual void Add(TEntity entity)
    {
        _dbSet.Add(entity);
        _context.Entry(entity).State = EntityState.Added;
    }
    public virtual async Task AddAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        await _dbSet.AddAsync(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual TEntity Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _dbSet.Update(entity);
        return entity;
    }

    public virtual TEntity Remove(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _dbSet.Remove(entity);
        return entity;
    }
    public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = GetAll(predicate, true);
        RemoveRange(entities);
    }
    public async Task<IEnumerable<TEntity>> RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await GetAllAsync(predicate, true);
        await Task.Run(() => RemoveRange(entities));
        return entities;
    }
    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task<bool> AnyAsync()
    {
        return await _dbSet.AnyAsync();
    }
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
    public bool Any()
    {
        return _dbSet.Any();
    }
    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }
    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.CountAsync(predicate);
    }
    public int Count()
    {
        return _dbSet.Count();
    }
    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Count(predicate);
    }
    public async Task<double> AverageAsync(Expression<Func<TEntity, double>> selector)
    {
        return await _dbSet.AverageAsync(selector);
    }
    public async Task<double> AverageAsync(Expression<Func<TEntity, int>> selector)
    {
        return await _dbSet.AverageAsync(selector);
    }
    public async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector)
    {
        return await _dbSet.AverageAsync(selector);
    }
    public async Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AverageAsync(selector);
    }
    public async Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AverageAsync(selector);
    }
    public async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AverageAsync(selector);
    }
    public async Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AverageAsync(selector);
    }
    public async Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AverageAsync(selector);
    }
    public async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AverageAsync(selector);
    }
    public double Average(Expression<Func<TEntity, double>> selector)
    {
        return _dbSet.Average(selector);
    }
    public double Average(Expression<Func<TEntity, int>> selector)
    {
        return _dbSet.Average(selector);
    }
    public decimal Average(Expression<Func<TEntity, decimal>> selector)
    {
        return _dbSet.Average(selector);
    }
    public double Average(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Average(selector);
    }
    public double Average(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Average(selector);
    }
    public decimal Average(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Average(selector);
    }
    public double Average(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Average(selector);
    }
    public double Average(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Average(selector);
    }
    public decimal Average(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Average(selector);
    }
    public async Task<double> SumAsync(Expression<Func<TEntity, double>> selector)
    {
        return await _dbSet.SumAsync(selector);
    }
    public async Task<int> SumAsync(Expression<Func<TEntity, int>> selector)
    {
        return await _dbSet.SumAsync(selector);
    }
    public async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector)
    {
        return await _dbSet.SumAsync(selector);
    }
    public async Task<double> SumAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.SumAsync(selector);
    }
    public async Task<int> SumAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.SumAsync(selector);
    }
    public async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.SumAsync(selector);
    }
    public async Task<double> SumAsync(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.SumAsync(selector);
    }
    public async Task<int> SumAsync(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.SumAsync(selector);
    }
    public async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.SumAsync(selector);
    }
    public double Sum(Expression<Func<TEntity, double>> selector)
    {
        return _dbSet.Sum(selector);
    }
    public int Sum(Expression<Func<TEntity, int>> selector)
    {
        return _dbSet.Sum(selector);
    }
    public decimal Sum(Expression<Func<TEntity, decimal>> selector)
    {
        return _dbSet.Sum(selector);
    }
    public double Sum(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Sum(selector);
    }
    public int Sum(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return (int)query.Sum(selector);
    }
    public decimal Sum(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Sum(selector);
    }
    public double Sum(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Sum(selector);
    }
    public int Sum(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return (int)query.Sum(selector);
    }
    public decimal Sum(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Sum(selector);
    }
    public async Task<double> MinAsync(Expression<Func<TEntity, double>> selector)
    {
        return await _dbSet.MinAsync(selector);
    }
    public async Task<int> MinAsync(Expression<Func<TEntity, int>> selector)
    {
        return await _dbSet.MinAsync(selector);
    }
    public async Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> selector)
    {
        return await _dbSet.MinAsync(selector);
    }
    public async Task<double> MinAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MinAsync(selector);
    }
    public async Task<int> MinAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MinAsync(selector);
    }
    public async Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MinAsync(selector);
    }
    public async Task<double> MinAsync(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MinAsync(selector);
    }
    public async Task<int> MinAsync(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MinAsync(selector);
    }
    public async Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MinAsync(selector);
    }
    public double Min(Expression<Func<TEntity, double>> selector)
    {
        return _dbSet.Min(selector);
    }
    public int Min(Expression<Func<TEntity, int>> selector)
    {
        return _dbSet.Min(selector);
    }
    public decimal Min(Expression<Func<TEntity, decimal>> selector)
    {
        return _dbSet.Min(selector);
    }
    public double Min(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Min(selector);
    }
    public int Min(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return (int)query.Min(selector);
    }
    public decimal Min(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Min(selector);
    }
    public double Min(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Min(selector);
    }
    public int Min(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return (int)query.Min(selector);
    }
    public decimal Min(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Min(selector);
    }
    public async Task<double> MaxAsync(Expression<Func<TEntity, double>> selector)
    {
        return await _dbSet.MaxAsync(selector);
    }
    public async Task<int> MaxAsync(Expression<Func<TEntity, int>> selector)
    {
        return await _dbSet.MaxAsync(selector);
    }
    public async Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector)
    {
        return await _dbSet.MaxAsync(selector);
    }
    public async Task<double> MaxAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MaxAsync(selector);
    }
    public async Task<int> MaxAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MaxAsync(selector);
    }
    public async Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MaxAsync(selector);
    }
    public async Task<double> MaxAsync(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MaxAsync(selector);
    }
    public async Task<int> MaxAsync(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MaxAsync(selector);
    }
    public async Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.MaxAsync(selector);
    }
    public double Max(Expression<Func<TEntity, double>> selector)
    {
        return _dbSet.Max(selector);
    }
    public int Max(Expression<Func<TEntity, int>> selector)
    {
        return _dbSet.Max(selector);
    }
    public decimal Max(Expression<Func<TEntity, decimal>> selector)
    {
        return _dbSet.Max(selector);
    }
    public double Max(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Max(selector);
    }
    public int Max(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return (int)query.Max(selector);
    }
    public decimal Max(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Max(selector);
    }
    public double Max(Expression<Func<TEntity, double>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Max(selector);
    }
    public int Max(Expression<Func<TEntity, int>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return (int)query.Max(selector);
    }
    public decimal Max(Expression<Func<TEntity, decimal>> selector, params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.Max(selector);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public int Save()
    {
        return _context.SaveChanges();
    }

    public IEnumerable<TEntity> Sort(IEnumerable<TEntity> entities, string? orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return entities;
        }
        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var param in orderParams)
        {
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(param.Trim(), StringComparison.InvariantCultureIgnoreCase));
            if (objectProperty == null)
            {
                continue;
            }
            entities = entities.OrderBy(b => objectProperty.GetValue(b, null));
        }
        return entities;
    }
}
