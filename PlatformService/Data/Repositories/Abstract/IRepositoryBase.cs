using System.Linq.Expressions;

namespace PlatformService.Data.Repositories.Abstract;


public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes);
    Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges, params string[] includes);
    Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges, params Expression<Func<TEntity, object>>[] includes);

    IEnumerable<TEntity> GetAll(bool trackChanges);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool trackChanges);
    IEnumerable<TEntity> GetAll(bool trackChanges, params Expression<Func<TEntity, object>>[] includes);
    IEnumerable<TEntity> GetAll(bool trackChanges, params string[] includes);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes);


    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes);

    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackChanges);
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params Expression<Func<TEntity, object>>[] includes);
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackChanges, params string[] includes);

    Task AddAsync(TEntity entity);
    void Add(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void AddRange(IEnumerable<TEntity> entities);

    TEntity Update(TEntity entity);

    TEntity Remove(TEntity entity);
    void RemoveRange(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate);
    void RemoveRange(IEnumerable<TEntity> entities);

    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    int Count();
    int Count(Expression<Func<TEntity, bool>> predicate);

    Task<double> AverageAsync(Expression<Func<TEntity, double>> selector);
    Task<double> AverageAsync(Expression<Func<TEntity, int>> selector);
    Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector);
    Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, params string[] includes);
    Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, params string[] includes);
    Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    double Average(Expression<Func<TEntity, double>> selector);
    double Average(Expression<Func<TEntity, int>> selector);
    decimal Average(Expression<Func<TEntity, decimal>> selector);
    double Average(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    double Average(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    decimal Average(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    double Average(Expression<Func<TEntity, double>> selector, params string[] includes);
    double Average(Expression<Func<TEntity, int>> selector, params string[] includes);
    decimal Average(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    Task<double> SumAsync(Expression<Func<TEntity, double>> selector);
    Task<int> SumAsync(Expression<Func<TEntity, int>> selector);
    Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector);
    Task<double> SumAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<int> SumAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<double> SumAsync(Expression<Func<TEntity, double>> selector, params string[] includes);
    Task<int> SumAsync(Expression<Func<TEntity, int>> selector, params string[] includes);
    Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    double Sum(Expression<Func<TEntity, double>> selector);
    int Sum(Expression<Func<TEntity, int>> selector);
    decimal Sum(Expression<Func<TEntity, decimal>> selector);
    double Sum(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    int Sum(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    decimal Sum(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    double Sum(Expression<Func<TEntity, double>> selector, params string[] includes);
    int Sum(Expression<Func<TEntity, int>> selector, params string[] includes);
    decimal Sum(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    Task<double> MinAsync(Expression<Func<TEntity, double>> selector);
    Task<int> MinAsync(Expression<Func<TEntity, int>> selector);
    Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> selector);
    Task<double> MinAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<int> MinAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<double> MinAsync(Expression<Func<TEntity, double>> selector, params string[] includes);
    Task<int> MinAsync(Expression<Func<TEntity, int>> selector, params string[] includes);
    Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    double Min(Expression<Func<TEntity, double>> selector);
    int Min(Expression<Func<TEntity, int>> selector);
    decimal Min(Expression<Func<TEntity, decimal>> selector);
    double Min(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    int Min(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    decimal Min(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    double Min(Expression<Func<TEntity, double>> selector, params string[] includes);
    int Min(Expression<Func<TEntity, int>> selector, params string[] includes);
    decimal Min(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    Task<double> MaxAsync(Expression<Func<TEntity, double>> selector);
    Task<int> MaxAsync(Expression<Func<TEntity, int>> selector);
    Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector);
    Task<double> MaxAsync(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<int> MaxAsync(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    Task<double> MaxAsync(Expression<Func<TEntity, double>> selector, params string[] includes);
    Task<int> MaxAsync(Expression<Func<TEntity, int>> selector, params string[] includes);
    Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    double Max(Expression<Func<TEntity, double>> selector);
    int Max(Expression<Func<TEntity, int>> selector);
    decimal Max(Expression<Func<TEntity, decimal>> selector);
    double Max(Expression<Func<TEntity, double>> selector, params Expression<Func<TEntity, object>>[] includes);
    int Max(Expression<Func<TEntity, int>> selector, params Expression<Func<TEntity, object>>[] includes);
    decimal Max(Expression<Func<TEntity, decimal>> selector, params Expression<Func<TEntity, object>>[] includes);
    double Max(Expression<Func<TEntity, double>> selector, params string[] includes);
    int Max(Expression<Func<TEntity, int>> selector, params string[] includes);
    decimal Max(Expression<Func<TEntity, decimal>> selector, params string[] includes);

    Task<bool> AnyAsync();
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    bool Any();
    bool Any(Expression<Func<TEntity, bool>> predicate);

    int Save();
    Task<int> SaveAsync();

    IEnumerable<TEntity> Sort(IEnumerable<TEntity> books, string? orderByQueryString);
}

