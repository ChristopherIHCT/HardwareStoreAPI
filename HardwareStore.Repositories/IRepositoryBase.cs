using System.Linq.Expressions;
using HardwareStore.Entities;

namespace HardwareStore.Repositories;

public interface IRepositoryBase<TEntity>
    where TEntity : EntityBase
{

    Task<ICollection<TEntity>> ListAsync();

    Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);

    Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
           Expression<Func<TEntity, bool>> predicate,
           Expression<Func<TEntity, TInfo>> selector,
           Expression<Func<TEntity, TKey>> orderby,
           int page, int rows,
           string? relationships = null);

    Task<TEntity?> FindByIdAsync(int id);

    Task<int> AddAsync(TEntity entity);

    Task UpdateAsync();

    Task UpdateAsync(int id, TEntity entity);

    Task DeleteAsync(int id);
}