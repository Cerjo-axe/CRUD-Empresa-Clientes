using Domain.Models;

namespace Domain.Interfaces.Repository;

public interface IBaseRepository<TEntity> where TEntity : Base
{
    Task Add(TEntity obj);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetbyId(string id);
    Task Update(TEntity obj);
    Task Delete(TEntity obj);
}

